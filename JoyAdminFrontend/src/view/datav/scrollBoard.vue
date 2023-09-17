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
  computed: {
    config () {
      let config = {
        header: [],
        data: [],
        index: true,
        columnWidth: [43, 170, 200, 150],
        align: ['center'],
        rowNum: 7,
        headerBGC: '#1981f6',
        headerHeight: 45,
        oddRowBGC: 'rgba(0, 44, 81, 0.8)',
        evenRowBGC: 'rgba(10, 29, 50, 0.8)'
      }
      let data = this.scrollData || []
      if (Array.isArray(data)) {
        if (data.length > 0) {
          let firstData = data[0]
          let header = Object.keys(firstData)
          config.header = header
        }
        let dataArray = []
        for (const item of data) {
          dataArray.push(Object.values(item))
        }
        config.data = dataArray
      } else {
        config.data = []
      }
      return config
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
