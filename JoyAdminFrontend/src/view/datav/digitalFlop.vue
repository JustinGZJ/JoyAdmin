<template>
  <div id="digital-flop">
    <div
      class="digital-flop-item"
      v-for="item in digitalFlopData"
      :key="item.title"
    >
      <div class="digital-flop-title">{{ item.title }}</div>
      <div class="digital-flop">
        <dv-digital-flop
          :config="item.number"
          style="width: 100px; height: 50px"
        />
        <div class="unit">{{ item.unit }}</div>
      </div>
    </div>
    <dv-decoration-10 />
  </div>
</template>

<script>
import configs from '@/config/digitalFlopConfig'
export default {
  name: 'DigitalFlop',
  props: {
    digitalData: {
      type: Object,
      default: function () {
        return {}
      } }
  },
  data () {
    return {
      colors: ['#f46827', '#4d99fc', '#40fbee'],
      digitalFlopData: []
    }
  },
  created: function () {
  },
  watch: {
    digitalData (newValue, oldValue) {
      this.createData(newValue)
    }
  },
  methods: {
    createData (digitalData) {
      //   debugger
      let data = []
      for (const digitalDataKey in digitalData) {
        let cfg = configs[digitalDataKey] || undefined
        let color = this.colors[1]
        if (cfg !== undefined) {
          let { '上限': upper, '下限': lower, '小数位数': fixed, '单位': unit } = cfg
          // 超过上限
          if (upper !== undefined && digitalData[digitalDataKey] > upper) {
            color = this.colors[2]
          }
          // 低于下限
          if (lower !== undefined && digitalData[digitalDataKey] < lower) {
            color = this.colors[0]
          }
          data.push(
            {
              title: digitalDataKey,
              number: {
                number: [digitalData[digitalDataKey]],
                toFixed: fixed || 0,
                content: '{nt}',
                textAlign: 'right',
                style: {
                  fill: color,
                  fontWeight: 'bold'
                }
              },
              unit: unit
            }
          )
        }
      }
      this.digitalFlopData = data
    }

  },
  mounted () {
    const { createData } = this
    createData()
  }
}
</script>

<style lang="less">
#digital-flop {
  position: relative;
  height: 15%;
  flex-shrink: 0;
  display: flex;
  justify-content: space-between;
  align-items: center;
  background-color: rgba(6, 30, 93, 0.5);

  .dv-decoration-10 {
    position: absolute;
    width: 95%;
    left: 2.5%;
    height: 2px;
    bottom: 0px;
  }

  .digital-flop-item {
    width: 11%;
    height: 80%;
    display: flex;
    flex-direction: column;
    justify-content: center;
    align-items: center;
    border-left: 2px solid rgb(6, 30, 93);
    border-right: 2px solid rgb(6, 30, 93);
  }

  .digital-flop-title {
    font-size: 10px;
    margin-bottom: 10px;
  }

  .digital-flop {
    display: flex;
  }

  .unit {
    margin-left: 5px;
    display: flex;
    align-items: flex-end;
    box-sizing: border-box;
    padding-bottom: 7px;
  }
}
</style>
