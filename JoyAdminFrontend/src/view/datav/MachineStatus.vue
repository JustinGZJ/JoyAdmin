<template>
  <div id="status">
    <dv-full-screen-container>
      <TopHeader title="运行情况"/>
      <dv-scroll-board :config="option" style="height: 500px"/>
    </dv-full-screen-container>
  </div>
</template>

<script>
// eslint-disable-next-line camelcase
import Current_data from '@/api/get_status'
import TopHeader from '@/view/datav/topHeader.vue'

export default {
  name: 'MyScrollBoard',
  components: { TopHeader },
  data () {
    return {
      status: [],
      timer: null
    }
  },
  computed: {
    option () {
      return {
        'header': ['名称', '平均周期', '报警时间', '停机时间', '运行时间', '预设数量', '进料数量', '出料数量', 'NG数量', '合格率', '报警状态', '运行状态'],
        'data': this.status,
        'columnWidth': [100, 80, 120, 100, 100, 120, 100, 100, 100, 100, 100, 100, 120, 100, 100],
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
        Current_data.get_by_key(`运行情况`).then(res => {
          this.status = res.data
          let groupValuesByStationData = Current_data.groupValuesByStation(this.status)
          this.status = Object.entries(groupValuesByStationData).map(([key, value]) => {
            const row = [key]
            this.option.header.slice(1).forEach(header => {
              row.push(value[header] !== undefined ? value[header] : '-')
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
