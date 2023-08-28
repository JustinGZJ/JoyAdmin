<template>
  <div id="water-level-chart">
    <div class="water-level-chart-title">产量达成情况</div>

    <div class="water-level-chart-details">累计完成<span>{{ product }}</span>个</div>

    <div class="chart-container">
      <dv-water-level-pond :config="config" />
    </div>
  </div>
</template>

<script>
export default {
  name: 'WaterLevelChart',
  props: {
    waterLevel: {
      type: Object,
      default: () => {
      }
    }
  },
  watch: {
    waterLevel (newValue) {
      let { product, plan } = newValue
      let percent = Math.floor((product || 0) * 100 / (plan || 1))
      this.config = {
        data: [percent],
        shape: 'round',
        waveHeight: 20,
        waveNum: 3
      }
      this.product = Math.round(product) || 0
    }
  },
  data () {
    return {
      product: 0,
      config: {
        data: [0],
        shape: 'round',
        waveHeight: 20,
        waveNum: 3
      }
    }
  }
}
</script>

<style lang="less">
#water-level-chart {
  width: 20%;
  box-sizing: border-box;
  margin-left: 20px;
  background-color: rgba(6, 30, 93, 0.5);
  border-top: 2px solid rgba(1, 153, 209, 0.5);
  display: flex;
  flex-direction: column;

  .water-level-chart-title {
    font-weight: bold;
    height: 50px;
    display: flex;
    align-items: center;
    font-size: 20px;
    justify-content: center;
  }

  .water-level-chart-details {
    height: 15%;
    display: flex;
    justify-content: center;
    font-size: 17px;
    align-items: flex-end;

    span {
      font-size: 35px;
      font-weight: bold;
      color: #58a1ff;
      margin: 0 5px -5px;
    }
  }

  .chart-container {
    flex: 1;
    display: flex;
    justify-content: center;
    align-items: center;
  }

  .dv-water-pond-level {
    max-width: 90%;
    width: 200px;
    height: 200px;
    border: 10px solid #19c3eb;
    border-radius: 50%;

    ellipse {
      stroke: transparent !important;
    }

    text {
      font-size: 40px;
    }
  }
}
</style>
