<template>
  <div id="status">
    <dv-full-screen-container>
      <TopHeader title="运行情况"/>
<!--      <product-by-hour/>-->
      <dv-scroll-board :config="option" style="height: 75%"/>
    </dv-full-screen-container>
  </div>
</template>

<script>
// eslint-disable-next-line camelcase
import Current_data from '@/api/get_status'
import TopHeader from '@/view/datav/topHeader.vue'
import ProductByHour from '@/view/datav/ProductByHour.vue'

export default {
  name: 'MyScrollBoard',
  components: { ProductByHour, TopHeader },
  data () {
    return {
      Data: [],
      status: [],
      timer: null,
      header: ['名称', '平均周期', '报警时间', '停机时间', '运行时间', '预设数量', '进料数量', '出料数量', 'NG数量', '合格率', '报警状态', '运行状态']
    }
  },
  computed: {
    option () {
      return {
        'header': ['名称', '平均周期(s)', '报警时间(min)', '停机时间(min)', '运行时间(min)', '预设数量(个)', '进料数量(个)', '出料数量(个)', 'NG数量(个)', '合格率(%)', '报警状态', '运行状态'],
        'data': this.status,
        'columnWidth': [200, 120, 120, 120, 120, 120, 120, 120, 120, 120, 120, 120, 120, 120, 120],
        'align': ['center'],
        'rowNum': 14,
        headerBGC: '#1981f6',
        headerHeight: 45,
        oddRowBGC: 'rgba(0, 44, 81, 0.8)',
        evenRowBGC: 'rgba(10, 29, 50, 0.8)'
      }
    }
  },
  methods: {
    getData () {
      try {
        Current_data.getByKey(`运行情况`).then(res => {
          this.Data = res.data
          let groupValuesByStationData = Current_data.groupValuesByStation(this.Data)
          this.status = Object.entries(groupValuesByStationData).map(([key, value]) => {
            const row = [key]
            this.header.slice(1).forEach(header => {
              if(header.includes("数量")){
                row.push(value[header].toFixed(0))
                return
              }
              if (header === '报警状态') {
                if (value[header]) {
                  row.push('报警')
                } else {
                  row.push('正常')
                }
                return
              }
              if (header === '运行状态') {
                if (value[header]) {
                  row.push('停止')
                } else {
                  row.push('工作')
                }
                return
              }
              if (typeof value[header] === 'number') {
                row.push(value[header].toFixed(2))
                return
              }
              row.push(value[header])
            })
            return row
          })
        })
      } catch (e) {
        console.log(e)
      }
    }
  },
  mounted () {
    this.getData()
    this.timer = setInterval(() => this.getData(), 5000)
  },
  destroyed () {
    clearInterval(this.timer)
  }
}
</script>

<style scoped>
#status {
  width: 100%;
  height: 100%;
  background-color: #030409;
  color: #fff;
  #dv-full-screen-container {
    background-image: url("./img/bg.png");
    background-size: 100% 100%;
    box-shadow: 0 0 3px blue;
    display: flex;
    flex-direction: column;
  }
}
</style>
