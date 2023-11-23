<template>
  <div class="column-chart">
    <dv-charts :option="option" style="width: 100%;" />
  </div>
</template>
<script>
export default {
  name: 'ColumnChart',
  props: {
    columnData: {
      type: Object,
      default: () => {
      }
    }
  },
  data() {
    return {}
  },
  methods: {},
  computed: {
    option() {
      let option = {
        title: {
          text: '不良分布',
          // 设置标题颜色为白色
          style: {
            fill: '#fff',
            fontSize: 20,
            fontWeight: 'bold',
            textAlign: 'right',
            textBaseline: 'bottom'
          }
        },
        xAxis: [{
          data: [],


          axisLine: {
            show: false
          },
          axisTick: {
            show: false
          },
          axisLabel: {
            style: {
              fill: '#fff'
            }
          }
        }, {
          data: [''],
          boundaryGap: false,
          axisLine: {
            show: false
          },
          axisTick: {
            show: false
          },
          axisLabel: {
            show: false,
            style: {
              fill: '#fff'
            }
          }
        }],
        yAxis: [
          {
            name: '数量(个)',
            data: 'value',
            position: 'left',
            splitLine: {
              show: false
            },
            min: 0,
            axisLine: {
              show: false
            },
            axisLabel: {
              formatter: '{value}',
              style: {
                fill: '#fff'
              }
            },
            nameTextStyle: {
              fill: '#fff'
            }
          },
          {
            name: '趋势(%)',
            data: 'value',
            position: 'right',
            max: 100,
            min: 0,
            splitLine: {
              show: false
            },
            axisLine: {
              show: false
            },
            axisLabel: {
              formatter: '{value}%',
              style: {
                fill: '#fff'
              }
            },
            axisTick: {
              show: false
            },
            nameTextStyle: {
              fill: '#fff'
            }
          }
        ],
        series: [
          {
            name: '趋势',
            data: [0],
            type: 'line',
            smooth: false,


            lineArea: {
              show: true,
              gradient: ['rgba(251, 114, 147, 1)', 'rgba(251, 114, 147, 0)']
            },
            lineStyle: {
              stroke: 'rgba(251, 114, 147, 1)',
              lineDash: [3, 3]
            },
            linePoint: {
              style: {
                stroke: 'rgba(251, 114, 147, 1)'
              }
            },
            yAxisIndex: 1,
            xAxisIndex: 1,
          },
          {
            name: '数量',
            data: [],
            type: 'bar',
            barGap: '0%',
            barCategoryGap: '0%',

            gradient: {
              color: ['rgba(103, 224, 227, .6)', 'rgba(103, 224, 227, .1)']
            },
            barStyle: {
              stroke: 'rgba(103, 224, 227, 1)'
            },
            label: {
              show: true,
              position: 'top'
            }
          }
        ]
      }
      let trend = 0
      // option.yAxis[0].max=vs[0][1]+10
      for (const newValueKey in this.columnData) {
        option.xAxis[0].data.push(newValueKey)
        option.xAxis[1].data.push(newValueKey)
        //  option.series[0].data.push(this.columnData[newValueKey])
        let sum = Object.entries(this.columnData).reduce((sum, [key, value]) => {
          return sum + value
        }, 0)
        if (sum == 0)
          sum = 1
        trend += this.columnData[newValueKey]
        option.series[0].data.push(Math.round(trend * 100 / sum))
        option.series[1].data.push(this.columnData[newValueKey])
      }
      return option
    }
  },

  mounted() {
  }
}
</script>

<style scoped lang="less">
.column-chart {
  width: 50%;
  height: 100%;
  background-color: rgba(6, 30, 93, 0.5);
  border-top: 2px solid rgba(1, 153, 209, 0.5);
  box-sizing: border-box;

  .dv-charts-container {
    height: calc(~"100% - 50px");
  }
}
</style>
