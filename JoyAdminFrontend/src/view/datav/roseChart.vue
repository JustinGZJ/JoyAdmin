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
      let option = {
        series: [
          {
            type: 'pie',
            radius: '50%',
            roseSort: false,
            data: [
            ],
            insideLabel: {
              show: false
            },
            outsideLabel: {
              formatter: '{name} {percent}%',
              labelLineEndLength: 20,
              style: {
                fill: '#fff'
              },
              labelLineStyle: {
                stroke: '#fff'
              }
            },
            roseType: true
          }
        ],
        color: ['#da2f00', '#fa3600', '#ff4411', '#ff724c', '#541200', '#801b00', '#a02200', '#5d1400', '#b72700']
      }
      for (const newValueKey in newValue) {
        option.series[0].data.push({
          name: newValueKey,
          value: newValue[newValueKey]
        })
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
    height: 25px;
    font-weight: bold;
    text-indent: 10px;
    font-size: 10px;
    display: flex;
    align-items: center;
  }

  .dv-charts-container {
    height: calc(~"100% - 50px");
  }
}
</style>
