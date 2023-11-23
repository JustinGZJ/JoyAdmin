<template>
  <div id="scroll-board">
    <dv-scroll-board ref="scrollBoard" :config="config"></dv-scroll-board>
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
  data() {
    return {
      config: {
        header: ['时间','目标','产量','差值','合格率'],
        data: [],
        index: true,
        columnWidth: [100, 100, 100, 100, 100],
        align: ['center'],
        rowNum: 7,
        headerBGC: '#1981f6',
        headerHeight: 45,
        oddRowBGC: 'rgba(0, 44, 81, 0.8)',
        evenRowBGC: 'rgba(10, 29, 50, 0.8)'
      }
    }
  },
  computed: {
    // config() {
    //   let config = {
    //     header: [],
    //     data: [],
    //     index: true,
    //     columnWidth: [100, 100, 100, 100, 100],
    //     align: ['center'],
    //     rowNum: 7,
    //     headerBGC: '#1981f6',
    //     headerHeight: 45,
    //     oddRowBGC: 'rgba(0, 44, 81, 0.8)',
    //     evenRowBGC: 'rgba(10, 29, 50, 0.8)'
    //   }
    //   let data = this.scrollData || []
    //   if (Array.isArray(data)) {
    //     if (data.length > 0) {
    //       let firstData = data[0]
    //       let header = Object.keys(firstData)
    //       config.header = header
    //     }
    //     let dataArray = []
    //     for (const item of data) {
    //       dataArray.push(Object.values(item))
    //     }
    //     config.data = dataArray
    //   } else {
    //     config.data = []
    //   }
    //   return config
    // }
  },
  watch:{
    scrollData(newValue){
      let data = newValue || []
      if (Array.isArray(data)) {
        if (data.length > 0) {
          // let firstData = data[0]
          // let header = Object.keys(firstData)
        //  this.$set(this.config,'header',header)
        }
        let dataArray = []
        console.log(data)
        for (const item of data) {
          if(item['差异']<0){
            dataArray.push(Object.values(item).map(v=>`<span style="color:red;font-weight: bold;">${v}</span>`))
          }else{
            dataArray.push(Object.values(item))
          }
         
        }
        this.$refs['scrollBoard'].updateRows(dataArray)
      } else {
      }     
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
