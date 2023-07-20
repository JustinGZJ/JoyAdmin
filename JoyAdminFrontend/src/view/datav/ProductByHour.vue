<template>
  <div ref="chart" style="height: 400px;"></div>
</template>

<script>
import * as echarts from 'echarts'
import 'echarts/theme/dark'
import { getProductByHours } from '@/api/get_status'

export default {
  mounted () {
    echarts.registerTheme('my_theme', {
      color: [
        '#1cf502',
        '#dee77d',
        '#02b6f1'],
      legend: {
        textStyle: {
          color: '#fff' // 图例文字颜色
        }
      },
      textStyle: {
        color: '#fff' // 设置文字颜色为白色
      }
    })
    this.chart = echarts.init(this.$refs.chart, 'my_theme')
    this.renderChart()
    this.timer = setInterval(this.renderChart, 5000)
  },
  beforeDestroy () {
    if (this.chart) {
      this.chart.dispose()
    }
    if (this.timer) {
      clearInterval(this.timer)
    }
  },
  methods: {
    renderChart () {
      getProductByHours().then(res => {
        let data = res.data
        this.production = data.product.map(item => item.value)
        this.target = data.target.map(item => item.value)
        this.achievementRate = data.product.map((item, index) => item.value / data.target[index].value * 100)
        const option = {
          legend: {
            data: ['目标值', '生产数', '达成率']
          },
          tooltip: {
            trigger: 'axis'
          },
          xAxis: {
            type: 'category',
            data: data.target.map(item => item.time)
          },
          yAxis: [
            {
              type: 'value',
              name: '目标值/生产数',
              axisLabel: {
                formatter: '{value}'
              }
            },
            {
              type: 'value',
              name: '达成率',
              min: 0,
              max: 100,
              interval: 25,
              axisLabel: {
                formatter: '{value}%'
              }
            }
          ],
          series: [
            {
              name: '目标值',
              type: 'bar',
              data: this.target,
              label: {
                show: true, // 显示标签
                position: 'top', // 标签位置
                textStyle: {
                  color: '#fff' // 标签文字颜色
                }
              }
            },
            {
              name: '生产数',
              type: 'bar',
              data: this.production,
              label: {
                show: true, // 显示标签
                position: 'top', // 标签位置
                textStyle: {
                  color: '#fff' // 标签文字颜色
                }
              }
            },
            {
              name: '达成率',
              type: 'line',
              yAxisIndex: 1,
              data: this.achievementRate
            }
          ]
        }
        this.chart.setOption(option)
      })
    }
  },
  data () {
    return {
      target: [100, 120, 150, 180, 200],
      production: [80, 100, 130, 160, 180],
      achievementRate: [80, 83, 87, 89, 90],
      chart: null,
      timer: null
    }
  }
}
</script>
