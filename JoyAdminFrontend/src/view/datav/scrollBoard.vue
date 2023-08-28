<template>
  <div id="scroll-board">
    <dv-scroll-board :config="config"></dv-scroll-board>
  </div>
</template>

<script>
export default {
  name: 'ScrollBoard',
  props: {
    scrollData: {
      type: Array,
      default: () => []
    }
  },
  watch: {
    scrollData (newValue) {
      this.createConfig(newValue)
    }
  },
  data () {
    return {
      config: {
        header: ['报警站位', '报警信息', '开始时间'],
        data: [
          // ['2019-07-01 19:25:00', '上料报警', '5', 'xxxxxxx']
        ],
        index: true,
        columnWidth: [43, 170, 200, 150],
        align: ['center'],
        rowNum: 7,
        headerBGC: '#1981f6',
        headerHeight: 45,
        oddRowBGC: 'rgba(0, 44, 81, 0.8)',
        evenRowBGC: 'rgba(10, 29, 50, 0.8)'
      }
    }
  },
  created: function () {
    // that = this
  },
  mounted () {

  },
  methods: {
    createConfig (newData) {
      let config = {
        header: ['报警站位', '报警信息', '开始时间'],
        data: [
          // ['2019-07-01 19:25:00', '上料报警', '5', 'xxxxxxx']
        ],
        index: true,
        columnWidth: [43, 170, 200, 150],
        align: ['center'],
        rowNum: 7,
        headerBGC: '#1981f6',
        headerHeight: 45,
        oddRowBGC: 'rgba(0, 44, 81, 0.8)',
        evenRowBGC: 'rgba(10, 29, 50, 0.8)'
      }
      let data = newData || []
      if (Array.isArray(data)) {
        if (data.length > 0) {
          let firstData = data[0]
          let header = Object.keys(firstData)
          config.header = header
          // this.$set(this.config, 'header', header)
        }
        let dataArray = []
        //    debugger
        for (const item of data) {
          dataArray.push(Object.values(item))
        }
        config.data = dataArray
        //   this.$set(this.config, 'data', dataArray)
      } else {
        config.data = []
      }
      this.config = config
    }
  }
}
</script>

<style lang="less">
#scroll-board {
  width: 50%;
  box-sizing: border-box;
  margin-left: 20px;
  height: 100%;
  overflow: hidden;
}
</style>
