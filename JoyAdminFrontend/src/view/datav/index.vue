<template>
  <div id="data-view">
    <dv-full-screen-container>
      <top-header :title="header" @on-date-change="handleDateChange" />
      <div class="main-content">
        <digital-flop :digital-data="digitalFlopData" />
        <div class="block-left-right-content">
          <ranking-board :ranking="randing" />
          <div class="block-top-bottom-content">
            <div class="block-top-content">
              <ColumnChart :column-data="columnData" />
              <scroll-board :scroll-data="scrollData" />
            </div>
            <cards :cards="cards" />
          </div>
        </div>
      </div>
    </dv-full-screen-container>
  </div>
</template>

<script>
import topHeader from './topHeader'
import digitalFlop from './digitalFlop'
import rankingBoard from './rankingBoard'
import roseChart from './roseChart'
import waterLevelChart from './waterLevelChart'
import scrollBoard from './scrollBoard'
import cards from './cards'
import currentData from '@/api/get_status'
import { FilterWorkOrderList } from '@/api/WorkOrder'
import { TelemetryDataDynamicFilter } from '../../api/telemetry'
import dayjs from 'dayjs'
import ColumnChart from '@/view/datav/ColumnChart.vue'

export default {
  name: 'DataView',
  components: {
    ColumnChart,
    topHeader,
    digitalFlop,
    rankingBoard,
    roseChart,
    waterLevelChart,
    scrollBoard,
    cards
  },
  data() {
    return {
      header: 'PT-A电机生产线',

      groupValuesByStationData: {},
      alarms: {},
      tmrStatus: undefined,
      order: undefined,
      tags: undefined,
      queryData: {},
      dateData: { current: true }
    }
  },
  computed: {
    digitalFlopData() {
      let data = {
        '工单号': '-',
        '产品名称': '-',
        '计划产量': 0,
        '出料数量': 0,
        'NG数量': 0,
        '合格率': 0,
        '完成率': 0,
        '剩余时间': 0
      }
      if (this.order) {
        data['工单号'] = this.order['WorkOrderNo']
        data['产品名称'] = this.order['ProductName']
        data['计划产量'] = this.order['PlanQuantity']
        data['出料数量'] = this.order['ActualQuantity']
        data['NG数量'] = this.order['NgQuantity']
        data['合格率'] = (this.order['ActualQuantity'] + this.order['NgQuantity']) === 0 ? 0 : this.order['ActualQuantity'] * 100 / (this.order['ActualQuantity'] + this.order['NgQuantity']);
        data['完成率'] = this.order['PlanQuantity'] === 0 ? 0 : this.order['ActualQuantity'] * 100 / this.order['PlanQuantity'];
        data['剩余时间'] = dayjs(this.order['FinishTime']).diff(dayjs(), 'hour', true)
      }
      return data
    },
    randing() {
      // 递归从tags对象和子对象中找到所有名字为“物料情况“的字段
      const findMaterial = (obj) => {
        let result = {}
        for (const key in obj) {
          if (key === '物料情况') {
            result = { ...result, ...obj[key] }
          } else if (typeof (obj[key]) === 'object') {
            result = { ...result, ...findMaterial(obj[key]) }
          }
        }
        return result
      }

      let obj = findMaterial(this.tags)

      // console.log(obj)
      const sortedArray = Object.entries(obj).sort((a, b) => {
        const func = (value) => {
          if (isNaN(value)) {
            return -1
          } else return value
        }
        return func(a[1]) - func(b[1])
      })
      // 获取前 10 个元素
      const topTen = sortedArray.slice(0, 10)
      // 将结果转换回对象格式（如果需要）
      return Object.fromEntries(topTen)
    },
    columnData() {
      let data = {
        "磁通量不良": 0,
        "转子推脱力不良": 0,
        "入壳高度检测不良": 0,
        "总成锁螺丝测试": 0,
        "定子综合测试不良": 0
      }
      const findReasons = (obj, reasons) => {
        for (let key in obj) {
          if (key === '不良统计') {
            for (let reason in obj[key]) {
              reasons[reason] = obj[key][reason]
            }
          } else if (typeof obj[key] === 'object') {
            findReasons(obj[key], reasons)
          }
        }
      }

      let obj2 = {}
      if (this.tags) {
        findReasons(this.tags, obj2)
        data = obj2
      }

      // 排序取 前5，其他合并为其他
      const sortedArray = Object.entries(data).sort((a, b) => {
        return b[1] - a[1]
      })
      const topFive = sortedArray.slice(0, 5)
      let other = 0
      for (let i = 5; i < sortedArray.length; i++) {
        other += sortedArray[i][1]
      }
      topFive.push(['其他不良', other])
      let topFiveObj = Object.fromEntries(topFive)
      return topFiveObj
    },
    cards() {
      const dataList = []
      for (const gpKey in this.groupValuesByStationData) {
        let gp = this.groupValuesByStationData[gpKey]
        const func = (value) => {
          if (typeof (value) === 'number') {
            return value
          } else {
            return 0
          }
        }
        let num = func(gp['出料数量'])
        let total = func(gp['出料数量']) + func(gp['NG数量'])
        let passRate = func(gp['合格率'])
        let title = gpKey
        let data = { title, num, total, passRate }
        dataList.push(data)
      }
      // console.log(obj)
      const sortedArray = dataList.sort((a, b) => {
        return b.num - a.num
      })
      // 排序
      // 获取前 5个元素
      return sortedArray
    },
    scrollData() {
      if (this.tags) {
        let data = []
        let plcdata = this.tags.总成.总成锁螺丝测试
        if (!plcdata) return []
        if (!plcdata.小时目标产量) return []
        if (Object.entries(plcdata.小时目标产量).length < 18) return []
        for (let i = 8; i < 18; i++) {
          let time = i.toString() + ':00'
          let target = Object.entries(plcdata.小时目标产量)[i][1]
          let product = Object.entries(plcdata.小时产量)[i][1]
          let rate = Object.entries(plcdata.每小时良率)[i][1]
          // 转换成百分比
          rate = (rate * 100).toFixed(1) + '%'
          data.push({ 时间: time, 目标: target, 实际产量: product, '差异': product - target, 良率: rate })
        }
        return data
      } else {
        return []
      }
      // return extractInfo(this.alarms)
    },
    filterOption() {
      let Option = {
        "page": 1,
        "size": 1,
        "sortExp": "Id desc"
      }
      if (!this.dateData.current) {
        let from = dayjs(this.dateData.fromtime).format('YYYY-MM-DD HH:mm:ss')
        let to = dayjs(this.dateData.totime).format('YYYY-MM-DD HH:mm:ss')
        Option['filterExp'] = `Time > "${from}" and Time < "${to}"`
      }
      return Option
    }
  },
  methods: {
    getData() {
      try {
        currentData.getByKey(`运行情况`).then(res => {
          this.status = res.data
          this.groupValuesByStationData = currentData.groupValuesByStation(this.status)
        })
      } catch (e) {
        console.log(e)
      }
    },
    getActiveWorkOrder() {
      FilterWorkOrderList({
        desc: true,
        filterProperty: 'Status',
        filterValue: '进行中',
        page: 1,
        size: 40,
        sortProperty: 'UpdatedTime'
      }).then(res => {
        this.order = res.data.Data.Items[0]
      })
    },
    getTags() {
      try {
        currentData.getTags().then(res => {
          this.tags = res.data
        })
      } catch (e) {
        console.log(e)
      }
    },

    getAlarms() {
      try {
        currentData.getAlarms('all').then(res => {
          this.alarms = res.data
        })
      } catch (e) {
        console.log(e)
      }
    },
    handleDateChange(data) {
      console.log(data)
      this.dateData = data
    },
    getAllData() {
      TelemetryDataDynamicFilter(this.filterOption).then(res => {
        if (res.status === 200) {
          this.queryData = res.data.Data.Items[0]
          this.status = this.queryData.Value.status
          this.groupValuesByStationData = currentData.groupValuesByStation(this.status)
          this.tags = this.queryData.Value.tags
          this.order = this.queryData.Value.workorder
        }
      })
    }
  },

  mounted() {
    this.getAllData()
    this.tmrStatus = setInterval(() => {
      this.getAllData()
    }, 5000);
  },
  destroyed() {
    clearInterval(this.tmrStatus)
  }
}
</script>

<style lang="less">
#data-view {
  width: 100%;
  height: 100%;
  background-color: #030409;
  color: #fff;

  #dv-full-screen-container {
    background-image: url("./img/bg.png");
    background-size: 100% 100%;
    box-shadow: 0 0 3px blue;
    display: flex;
    flex-direction: column;
  }

  .main-content {
    flex: 1;
    display: flex;
    flex-direction: column;
  }

  .block-left-right-content {
    flex: 1;
    display: flex;
    margin-top: 20px;
  }

  .block-top-bottom-content {
    flex: 1;
    display: flex;
    flex-direction: column;
    box-sizing: border-box;
    padding-left: 20px;
  }

  .block-top-content {
    height: 55%;
    display: flex;
    flex-grow: 0;
    box-sizing: border-box;
    padding-bottom: 20px;
  }
}
</style>
