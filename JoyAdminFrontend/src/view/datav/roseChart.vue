<template>
  <div id="rose-chart">
    <div class="rose-chart-title">故障分布</div>
    <dv-charts :option="option" />
  </div>
</template>
<script>
export default {
  name: 'RoseChart',
  props: {
    roseData: {
      type: Object,
      default: () => {}
    }
  },
  data () {
    return {
      option: {
      }
    }
  },
  methods: {
  },
  watch: {
    roseData (newValue) {
      const option = {
        xAxis: {
          name: '',
          data: [],
          axisLabel: {
            rotate: 45,
            style: {
              fill: '#fff',
              fontSize: 15
            }
          },
          axisTick: {
            show: false
          },
          axisLine: {
            show: false
          },
          splitLine: {
            show: false
          },
          nameTextStyle: {
            fill: '#fff',
            fontSize: 15
          }
        },
        yAxis: {
          name: '',
          data: 'value',
          axisTick: {
            show: false
          },
          axisLabel: {
            show: false
          },
          axisLine: {
            show: false
          },
          splitLine: {
            show: false
          },
          min: 0
        },
        series: [
          {
            data: [],
            type: 'bar',
            label: {
              show: true,
              position: 'top',
              style: {
                color: '#fff'
              }
            },
            gradient: {
              color: ['#fdd819', '#e80505']
            },
            barStyle: {
              radius: [10, 10, 0, 0]
            }
          }
        ]
      }

      for (const newValueKey in newValue) {
        option.xAxis.data.push(newValueKey)
        option.series[0].data.push(Math.round(newValue[newValueKey])
        )
      }
      this.option = option
    }
  },
  mounted () {
  }
}
</script>

<style lang="less">
#rose-chart {
  width: 30%;
  height: 100%;
  background-color: rgba(6, 30, 93, 0.5);
  border-top: 2px solid rgba(1, 153, 209, 0.5);
  box-sizing: border-box;

  .rose-chart-title {
    height: 50px;
    font-weight: bold;
    text-indent: 20px;
    font-size: 20px;
    display: flex;
    align-items: center;
  }

  .dv-charts-container {
    height: calc(~"100% - 50px");
  }
}
</style>
