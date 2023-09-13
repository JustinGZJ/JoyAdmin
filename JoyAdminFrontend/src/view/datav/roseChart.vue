<template>
  <div id="rose-chart">
    <div class="rose-chart-title">故障分布</div>
<!--    <dv-charts :option="option" />-->
    <dv-active-ring-chart :config="config" style="width: 100%;height: 100%" />
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
      option: {},
      config: {
        radius: '60%',
        activeRadius: '55%',
        data: [],
        digitalFlopStyle: {
          fontSize: 20
        }
      }
    }
  },
  methods: {
  },
  watch: {
    roseData (newValue) {
      let config = {
        radius: '60%',
        activeRadius: '55%',
        data: [
        ],
        digitalFlopStyle: {
          fontSize: 20
        }
      }
      for (const newValueKey in newValue) {
        config.data.push({
          name: newValueKey,
          value: newValue[newValueKey]
        })
      }
      this.config = config
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
