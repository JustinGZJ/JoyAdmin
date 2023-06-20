<template>
  <div ref="dom" class="charts chart-bar"></div>
</template>

<script>
import echarts from 'echarts'
import tdTheme from './theme.json'
import { on, off } from '@/libs/tools'
echarts.registerTheme('tdTheme', tdTheme)
export default {
  name: 'passratechart',
  props: {
    value: [],
    text: String,
    subtext: String
  },
  data () {
    return {
      dom: null,
      option: null
    }
  },
  methods: {
    resize () {
      this.dom.resize()
    }
  },
  watch: {
    value (newValue, oldValue) {

    }
  },
  mounted () {
    this.$nextTick(() => {
      let xAxisData = this.value.map(_ => _.Start)
      let bar1Data = this.value.map(_ => _.Ok)
      let bar2Data = this.value.map(_ => _.Ng)
      let lineData = this.value.map(_ => (_.Ok / (_.Ok + _.Ng)))

      const colors = ['#5470C6', '#91CC75', '#EE6666']
      this.option = {
        color: colors,
        tooltip: {
          trigger: 'axis',
          axisPointer: {
            type: 'cross'
          }
        },
        grid: {
          right: '20%'
        },
        toolbox: {
          feature: {
            dataView: { show: true, readOnly: false },
            restore: { show: true },
            saveAsImage: { show: true }
          }
        },
        legend: {
          data: ['合格数', '不合格数', '合格率']
        },
        xAxis: [
          {
            type: 'category',
            axisTick: {
              alignWithLabel: true
            },
            // prettier-ignore
            data: xAxisData
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
                color: colors[0]
              }
            },
            axisLabel: {
              formatter: '{value} pcs'
            }
          },
          {
            type: 'value',
            name: '合格率',
            position: 'right',
            alignTicks: true,
            axisLine: {
              show: true,
              lineStyle: {
                color: colors[2]
              }
            },
            axisLabel: {
              formatter: '{value} °C'
            }
          }
        ],
        series: [
          {
            name: '合格',
            type: 'bar',
            data: bar1Data
          },
          {
            name: '不合格',
            type: 'bar',
            data: bar2Data
          },
          {
            name: '合格率',
            type: 'line',
            yAxisIndex: 1,
            data: lineData
          }
        ]
      }

      this.dom = echarts.init(this.$refs.dom, 'tdTheme')
      this.dom.setOption(option)
      on(window, 'resize', this.resize)
    })
  },
  beforeDestroy () {
    off(window, 'resize', this.resize)
  }
}
</script>
