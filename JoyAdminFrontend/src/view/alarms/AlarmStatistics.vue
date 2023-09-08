<script>
import dayjs from 'dayjs'
import Alarm from '@/api/alarm'
import { ChartBar } from '@/components/charts'
import ChartPie from '_c/charts/pie.vue'
import { getStations } from '@/api/Process'

export default {
  data () {
    return {
      station: '所有',
      stations: [],
      date_range: ['2016-01-01', '2016-02-15'],
      alarmData: {},
      alarmCount: [],
      text: '报警分布',
      subtext: '次数'
    }
  },
  computed: {
    chartData () {
      if (Object.keys(this.alarmData).length === 0) {
        return {}
      } else {
        let result = {}
        for (const dataKey in this.alarmData) {
          let obj = {}
          const data = this.alarmData[dataKey]
          for (const item of data) {
            obj[item['Message']] = item['Count']
          }
          result[dataKey] = obj
        }
        return result
      }
    },
    chartCount () {
      if (this.alarmCount.length === 0) {
        return []
      } else {
        const arr = this.alarmCount.map(item => {
          return {
            name: item['Station'],
            value: item['Count']
          }
        })
        return [arr]
      }
    }
  },
  methods: {
    query () {
      let station = this.station
      if (this.station === '所有') {
        station = ''
      }
      Alarm.GetAlarmFreq({ 'Station': station, 'Start': this.date_range[0], 'End': this.date_range[1] }).then(res => {
        this.alarmData = res.data.Data
      })
      Alarm.GetAlarmCount({ 'Station': station, 'Start': this.date_range[0], 'End': this.date_range[1] }).then(res => {
        this.alarmCount = res.data.Data
      })
    },
    getStations () {
      getStations().then((res) => {
        const data = res.data
        if (res.status === 200) {
          this.stations = [{ value: '所有', label: '所有' }, ...(data.map((item) => {
            return {
              value: item,
              label: item
            }
          }))]
        } else {
          this.$Notice.error({
            title: data.Errors
          })
        }
      })
    }
  },
  mounted () {
    this.date_range = [dayjs().startOf('day').format(), dayjs().endOf('day').format()]
    this.getStations()
  },
  components: {
    ChartPie,
    ChartBar
  }
}

</script>

<template>
  <div>
    <div>
      <Select v-model="station" style="width:200px">
        <Option v-for="item in stations" :value="item.value" :key="item.value">{{ item.label }}</Option>
      </Select>
      <Date-picker type="datetimerange" v-model="date_range" format="yyyy-MM-dd HH:mm" placeholder="选择日期和时间"
                   style="width: 300px"></Date-picker>
      <Button @click.prevent="query">查询</Button>
    </div>
    <div class="chart-container">
      <div class="left" v-if="station==='所有'">
        <ChartPie v-for="pie  in chartCount" :key="pie" text="报警分布" subtext="报警次数"
                  style="width: 100%;height: 400px" :value="pie"></ChartPie>
      </div>
      <div class="right">
        <ChartBar v-for="(value,key) in chartData" :text="key" style="height: 300px;width: 100%" :value="value"
                  :key="key"/>
      </div>
    </div>
  </div>
</template>

<style scoped lang="less">
.chart-container {
  display: flex;
  .left {
    flex: 3;
  }
  .right {
    flex: 7;
  }
}
</style>
