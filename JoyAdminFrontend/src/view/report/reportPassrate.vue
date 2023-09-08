<template>
  <div>
    <div>
      <Select v-model="station" style="width:200px">
        <Option v-for="item in stations" :value="item.value" :key="item.value">{{ item.label }}</Option>
      </Select>
      <Date-picker type="datetimerange" v-model="date_range" format="yyyy-MM-dd HH:mm" placeholder="选择日期和时间"
                   style="width: 300px"></Date-picker>
      <Select v-model="aggregation" style="width:200px">
        <Option v-for="item in aggregationSet" :value="item.value" :key="item.value">{{ item.label }}</Option>
      </Select>
      <Button @click.prevent="query">查询</Button>
    </div>
    <div >
      <div class="chart-container" ref="chart"></div>
    </div>

  </div>
</template>
<script>
import Production from '@/api/production'
import dayjs from 'dayjs'
import * as echarts from 'echarts'
import { off, on } from '@/libs/tools'
import tdTheme from '@/components/charts/theme.json'
import { getStations } from '@/api/Process'
echarts.registerTheme('tdTheme', tdTheme)

export default {
  data () {
    return {
      station: '所有',
      stations: [],
      aggregation: 1,
      aggregationSet: [
        {
          value: 0,
          label: 'NONE'
        }, {
          value: 1,
          label: 'HOUR'
        }, {
          value: 2,
          label: 'DAY'
        }, {
          value: 3,
          label: 'WEEK'
        }, {
          value: 4,
          label: 'MONTH'
        }],
      date_range: ['2016-01-01', '2016-02-15'],
      Data: [],
      chart: null
    }
  },
  computed: {
    chartOptions () {
      return {
        tooltip: {
          trigger: 'axis',
          axisPointer: {
            type: 'cross',
            crossStyle: {
              color: '#999'
            }
          }
        },
        toolbox: {
          feature: {
            dataView: { show: true, readOnly: false },
            magicType: { show: true, type: ['line', 'bar'] },
            restore: { show: true },
            saveAsImage: { show: true }
          }
        },
        legend: {
          data: ['合格', '不合格', '合格率']
        },
        xAxis: [
          {
            type: 'category',
            axisTick: {
              alignWithLabel: true
            },
            axisLabel: {
              interval: 0, // 设置为0，强制显示所有标签
              rotate: 45, // 旋转45度，防止标签重叠
              textStyle: {
                fontSize: 10 // 调整字体大小
              } },
            data: this.Data.map(item => item.Start)
          }
        ],
        yAxis: [
          {
            type: 'value',
            name: '数量',
            position: 'left',
            alignTicks: true,
            axisLine: {
              show: true,
              lineStyle: {
                color: '#5470C6'
              }
            }
          },
          {
            type: 'value',
            name: '百分比',
            position: 'right',
            alignTicks: true,
            axisLine: {
              show: true,
              lineStyle: {
                color: '#5470C6'
              }
            }
          }
        ],
        series: [
          {
            name: '合格',
            type: 'bar',
            tooltip: {
              valueFormatter: function (value) {
                return value + '个'
              }
            },
            data: this.Data.map(item => item.Ok),
            barWidth: '30%',
            itemStyle: {
              normal: {
                barBorderRadius: 5
              }
            }
          },
          {
            name: '不合格',
            type: 'bar',
            tooltip: {
              valueFormatter: function (value) {
                return value + '个'
              }
            },
            data: this.Data.map(item => item.Ng),
            barWidth: '30%',
            itemStyle: {
              normal: {
                barBorderRadius: 5
              }
            }
          },
          {
            name: '合格率',
            type: 'line',
            yAxisIndex: 1,
            lineStyle: {
              normal: {
                connectNulls: true
              }
            },
            tooltip: {
              valueFormatter: function (value) {
                return value + '%'
              }
            },
            data: this.Data.map(item => (item.Ok + item.Ng) === 0 ? 0 : item.Ok / (item.Ok + item.Ng) * 100)
          }
        ]
      }
    },
    chartShow () {
      return this.Data.length > 0
    }
  },
  methods: {
    query () {
      let station = this.station
      if (this.station === '所有') {
        station = ''
      }
      this.loading = false
      Production.GetPassRates({
        Device: station,
        Start: this.date_range[0].toLocaleString(),
        End: this.date_range[1].toLocaleString(),
        Aggregation: this.aggregation
      }).then((res) => {
        this.loading = false
        const data = res.data
        if (data.StatusCode === 200) {
          console.log(data)
          this.Data = data.Data
        } else {
          this.$Notice.error({
            title: data.Errors
          })
        }
      })
    },
    resize () {
      if (this.chart) {
        this.chart.resize()
      }
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
    this.chart = echarts.init(this.$refs.chart, 'tdTheme')
    console.log(this.$refs.chart)
    this.getStations()
    this.query()
    this.$nextTick(() => {
      this.chart.setOption(this.chartOptions)
      on(window, 'resize', this.resize)
    })
  },
  beforeDestroy () {
    off(window, 'resize', this.resize)
    if (this.chart) {
      this.chart.dispose()
    }
  },
  watch: {
    date_range (val) {
      console.log(val)
      let diff = dayjs(val[1]).diff(dayjs(val[0]), 'day')
      if (diff <= 2) {
        this.aggregation = 1
      } else if (diff < 31) {
        this.aggregation = 2
      } else {
        this.aggregation = 3
      }
    },
    Data (val) {
      if (val === undefined || val === null || val.length === 0) {
        return
      }
      this.chart.setOption(this.chartOptions)
    }
  }
}

</script>

<style scoped lang="less">
.chart-container{
  height: 400px;
  width: 100%;
  margin-top: 20px;
}
</style>
