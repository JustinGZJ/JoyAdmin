<template>
  <div>
    <div>
      <Date-picker type="datetimerange" v-model="date_range" format="yyyy-MM-dd HH:mm" placeholder="选择日期和时间"
                   style="width: 300px"></Date-picker>
      <Button @click.prevent="query">查询</Button>
    </div>
    <div>
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
echarts.registerTheme('tdTheme', tdTheme)

export default {
  data () {
    return {
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
            data: this.Data.map(item => item.Device)
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
      this.loading = true
      Production.GetPassRateByDevice(dayjs(this.date_range[0]).format(), dayjs(this.date_range[1]).format()).then((res) => {
        this.loading = false
        const data = res.data
        if (data.StatusCode === 200) {
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
    }
  },
  mounted () {
    this.date_range = [dayjs().startOf('day').format(), dayjs().endOf('day').format()]
    this.query()

    this.$nextTick(() => {
      this.chart = echarts.init(this.$refs.chart)
      this.chart.setOption(this.chartOptions)
      on(window, 'resize', this.resize)
    })
  },
  beforeDestroy () {
    if (!this.chart) {
      return
    }
    this.chart.dispose()
    this.chart = null
    off(window, 'resize', this.resize)
  },
  watch: {
    Data () {
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
