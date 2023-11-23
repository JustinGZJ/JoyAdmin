<template>
  <div id="top-header">
    <dv-decoration-8 class="header-left-decoration" />
    <dv-decoration-5 class="header-center-decoration" />
    <dv-decoration-8 class="header-right-decoration" :reverse="true" />
    <div class="center-title">{{ title }}</div>
    <div class="current-time" @click="modalVisible = true">{{ currentTime }}</div>
    <Modal v-model="modalVisible" @on-ok="handleOk" title="选择日期区间">

      <div v-if="!current">
        <div>开始时间</div>
        <Date-picker type="datetime" v-model="fromtime" placeholder="选择日期和时间"></Date-picker>
        <div> 结束时间</div>
        <Date-picker type="datetime" v-model="totime" placeholder="选择日期和时间"></Date-picker>
      </div>
      <div>最新数据</div>
      <i-switch v-model="current">最新数据</i-switch>
    </Modal>
  </div>
</template>

<script>
import dayjs from 'dayjs';


export default {
  name: 'TopHeader',
  props: {
    title: {
      type: String,
      default: '电机生产线'
    }
  },
  data() {
    return {
      currentTime: '',
      current: true,
      modalVisible: false,
      fromtime: new Date(Date.now() - 86400000),
      totime: new Date()
    }
  },
  mounted() {
    this.updateTime()
    setInterval(this.updateTime, 1000)
  },
  methods: {
    updateTime() {
      const now = new Date();
      const hours = now.getHours().toString().padStart(2, '0');
      const minutes = now.getMinutes().toString().padStart(2, '0');
      const seconds = now.getSeconds().toString().padStart(2, '0');
      if (this.current) {
        this.currentTime = `${hours}:${minutes}:${seconds}`
      } else {
        this.currentTime = dayjs(this.fromtime).format('YYYY-MM-DD HH:mm') + ' - ' + dayjs(this.totime).format('YYYY-MM-DD HH:mm')
      }


    },
    handleOk() {
      var payload = {
        current: this.current,
        fromtime: this.fromtime,
        totime: this.totime
      }
      console.log(payload)
      this.$emit("on-date-change", payload)

    }
  }
}
</script>

<style lang="less">
#top-header {
  position: relative;
  width: 100%;
  height: 100px;
  display: flex;
  justify-content: space-between;
  flex-shrink: 0;

  .header-center-decoration {
    width: 40%;
    height: 60px;
    margin-top: 30px;
  }

  .header-left-decoration,
  .header-right-decoration {
    width: 25%;
    height: 60px;
  }

  .center-title {
    position: absolute;
    font-size: 30px;
    font-weight: bold;
    left: 50%;
    top: 15px;
    transform: translateX(-50%);
  }

  .current-time {
    position: absolute;
    font-size: 20px;
    right: 10%;
    top: 30%;
  }
}
</style>
