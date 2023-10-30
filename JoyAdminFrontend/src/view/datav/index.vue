<template>
  <div id="data-view">
    <dv-full-screen-container>
      <top-header :title="header"/>
      <div class="main-content">
        <digital-flop :digital-data="digitalFlopData"/>
        <div class="block-left-right-content">
          <ranking-board :ranking="randing"/>
          <div class="block-top-bottom-content">
            <div class="block-top-content">
<!--              <rose-chart :rose-data="roseData"/>-->
<!--              <water-level-chart :water-level="waterLevel"/>-->
              <ColumnChart :column-data="columnData"/>
              <scroll-board :scroll-data="scrollData"/>
            </div>
            <cards :cards="cards"/>
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
  data () {
    return {
      header: 'PT-A电机生产线',

      groupValuesByStationData: {},
      alarms: {},
      tmrStatus: undefined,
      order: undefined,
      tags: undefined
    }
  },
  computed: {
    digitalFlopData () {
      let data = {
        '工单号': '-',
        '产品名称': '-',
        '预设数量': 0,
        '出料数量': 0,
        'NG数量': 0,
        '合格率': 0,
        '完成率': 0,
        '剩余时间': 0
      }
      if (this.order) {
        data['工单号'] = this.order['WorkOrderNo']
        data['产品名称'] = this.order['ProductName']
        data['预设数量'] = this.order['PlanQuantity']
        data['出料数量'] = this.order['ActualQuantity']
        data['NG数量'] = this.order['NgQuantity']
        data['合格率'] = this.order['ActualQuantity'] * 100 / (this.order['ActualQuantity'] + this.order['NgQuantity'])
        data['完成率'] = this.order['ActualQuantity'] * 100 / this.order['PlanQuantity']
        data['剩余时间'] = dayjs(this.order['FinishTime']).diff(dayjs(), 'hour', true)
      }
      return data
    },
    randing () {
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
    columnData () {
      let data = {
        '焊锡不良': 10,
        '推脱力不良': 20,
        '定子铁芯缺料': 60,
        '定子绝缘物缺料': 30,
        '其他不良': 5
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
      return Object.fromEntries(topFive)
    },
    cards () {

      return [
        { title:"磁通量检测", num:292, total:300, passRate:293/300 },
        { title:"推脱力检测", num:295, total:300, passRate:295/300 },
        { title:"耐压检测", num:290, total:300, passRate: 290/300},
        { title:"反电动势检测", num:296, total:302, passRate:296/302 },
        { title:"反电动势检测", num:332, total:342, passRate: 287/342}
      ]
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
        return b.num / b.total - a.num / a.total
      })
      // 获取前 5个元素
      return sortedArray.slice(0, 5)
    },
    scrollData () {
      return [
        { '时间': '8:00', '目标': 60, '实际产量': 58, '差异': -2,"良率":"98%" },
        { '时间': '9:00', '目标': 60, '实际产量': 58, '差异': -2,"良率":"99%" },
        { '时间': '10:00', '目标': 60, '实际产量': 63, '差异': 3,"良率":"98%" },
        { '时间': '11:00', '目标': 60, '实际产量': 58, '差异': -2,"良率":"97%" },
        { '时间': '12:00', '目标': 60, '实际产量': 58, '差异': -2,"良率":"98%" },
        { '时间': '13:00', '目标': 60, '实际产量': 58, '差异': -2,"良率":"99%" },
        { '时间': '14:00', '目标': 60, '实际产量': 63, '差异': 3,"良率":"98%" },
        { '时间': '15:00', '目标': 60, '实际产量': 58, '差异': -2,"良率":"97%" },
        { '时间': '16:00', '目标': 60, '实际产量': 58, '差异': -2,"良率":"99%" },
        { '时间': '17:00', '目标': 60, '实际产量': 63, '差异': 3,"良率":"98%" },
      ]


      if (!this.tags) {
        // 返回测试数据
        return [
          { '时间': '8:00', '目标': 0, '实际产量': 0, '达成率': '0%' }
        ]
      }
      let products = Object.entries(this.tags['总成']['总成锁螺丝测试']['小时产量']).map(item => item[1])
      let targets = Object.entries(this.tags['总成']['总成锁螺丝测试']['小时目标产量']).map(item => item[1])
      let achievementRate = products.map((item, index) => (item / targets[index] * 100).toFixed(2) + '%')

      // 时间 目标 产量 达成率
      let data = []
      for (let i = 0; i < products.length; i++) {
        let time = i.toString() + ':00'
        let target = targets[i]
        let product = products[i]
        let rate = achievementRate[i]
        data.push({ 时间: time, 目标: target, 实际产量: product, 达成率: rate })
      }
      if (data.length === 0) {
        data.push({ 时间: '8:00', 目标: 0, 实际产量: 0, 达成率: '0%' })
      }
      return data
      // return extractInfo(this.alarms)
    }
  },
  methods: {
    getData () {
      try {
        currentData.getByKey(`运行情况`).then(res => {
          this.status = res.data
          this.groupValuesByStationData = currentData.groupValuesByStation(this.status)
        })
      } catch (e) {
        console.log(e)
      }
    },
    getActiveWorkOrder () {
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
    getTags () {
      try {
        currentData.getTags().then(res => {
          this.tags = res.data
        })
      } catch (e) {
        console.log(e)
      }
    },
    getAlarms () {
      try {
        currentData.getAlarms('all').then(res => {
          this.alarms = res.data
        })
      } catch (e) {
        console.log(e)
      }
    }
  },
  mounted () {
    this.getData()
    this.getAlarms()
    this.getActiveWorkOrder()
    this.getTags()
    this.tmrStatus = setInterval(() => {
      this.getData()
      this.getActiveWorkOrder()
      this.getAlarms()
      this.getTags()
    }, 5000)
  },
  destroyed () {
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
