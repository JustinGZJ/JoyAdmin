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
              <rose-chart :rose-data="roseData"/>
              <water-level-chart :water-level="waterLevel"/>
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

export default {
  name: 'DataView',
  components: {
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
      tmrAlarm: undefined
    }
  },
  computed: {
    digitalFlopData () {
      let keys = ['预设数量', '进料数量', '出料数量', 'NG数量', '合格率', '运行时间', '停机时间', '报警时间', '平均周期']
      let data = {}
      let tempData = this.groupValuesByStationData['总成3']
      if (tempData !== undefined) {
        for (const key of keys) {
          data[key] = this.groupValuesByStationData['总成3'][key] || '-'
        }
      }
      return data
    },
    randing () {
      // 从groupValuesByStationData数组中获取说有的周期时间字段
      let tempData = this.groupValuesByStationData || {}

      let obj = {}
      for (const key in tempData) {
        let data = tempData[key] || {}
        obj[key] = data['平均周期']
      }
      // console.log(obj)
      const sortedArray = Object.entries(obj).sort((a, b) => {
        const func = (value) => {
          if (isNaN(value)) {
            return -1
          } else return value
        }
        return func(b[1]) - func(a[1])
      })
      // 获取前 10 个元素
      const topTen = sortedArray.slice(0, 10)
      // 将结果转换回对象格式（如果需要）
      return Object.fromEntries(topTen)
    },
    roseData () {
      // 从groupValuesByStationData数组中获取说有的周期时间字段
      let tempData = this.groupValuesByStationData || {}

      let obj = {}
      for (const key in tempData) {
        let data = tempData[key] || {}
        if (!isNaN(data['报警时间'])) {
          obj[key] = data['报警时间']
        }
      }

      // console.log(obj)
      const sortedArray = Object.entries(obj).sort((a, b) => {
        const func = (value) => {
          if (isNaN(value)) {
            return -1
          } else return value
        }
        return func(b[1]) - func(a[1])
      })
      // 获取前 11 个元素
      const topTen = sortedArray.slice(0, 11)
      // 将结果转换回对象格式（如果需要）
      return Object.fromEntries(topTen)
    },
    waterLevel () {
      const tempData = this.groupValuesByStationData['总成3'] || {}
      //  const {plan:预设数量,product:出料数量}=this.groupValuesByStationData["总成3"]
      const func = (value, defaultValue = -1) => {
        if (isNaN(value)) {
          return defaultValue
        } else return value
      }
      const plan = func(tempData['预设数量'], 1)
      const product = func(tempData['出料数量'], 0)
      return { plan, product }
    },
    cards () {
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
        let total = func(gp['进料数量']) + func(gp['NG数量'])
        let passRate = func(gp['合格率'])
        let title = gpKey
        let data = { title, num, total, passRate }
        dataList.push(data)
      }
      // console.log(obj)
      const sortedArray = dataList.sort((a, b) => {
        const func = (value) => {
          if (isNaN(value)) {
            return -1
          } else return value
        }
        return func(b.passRate) - func(a.passRate)
      })
      // 获取前 5个元素
      return sortedArray.slice(0, 5)
    },
    scrollData () {
      const extractInfo = (json) => {
        const result = []
        for (const key in json) {
          const parts = key.split('.')
          const objectField = parts[1]
          const alarmContent = parts[3]
          const time = json[key].sourceTimestamp
          result.push({ objectField, alarmContent, time })
        }
        return result
      }
      return extractInfo(this.alarms)
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
    this.tmrStatus = setInterval(() => this.getData(), 5000)
    this.tmrAlarm = setInterval(() => this.getAlarms(), 5000)
  },
  destroyed () {
    clearInterval(this.tmrStatus)
    clearInterval(this.tmrAlarm)
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
