<template>
  <div>
    <Row :gutter="10">
      <i-col :xs="12" :md="8" :lg="4" v-for="(pr, i) in passrate" :key="`infor-${i}`" style="height: 120px;padding-bottom: 10px;">
        <card shadow  style="padding: 0" padding="0">
          <div style="padding:2px 5px; white-space: nowrap;background: rgb(85, 206, 128);font-size: 20px;font-weight: bold;color: white">{{ pr.Device }}</div>
          <div style="padding: 5px">
            <div style="font-size: 14px">
              <span >合格数:</span>
              <span >{{ pr.Ok }}</span>
            </div>
            <div style="font-size: 14px">
              <span >不合格:</span>
              <span >{{ pr.Ng }}</span>
            </div>
            <div style="font-size: 14px">
              <span >合格率:</span>
              <span >{{ pr.OkRate }}</span>
            </div>
          </div>

        </card>
      </i-col>
    </Row>
  </div>
</template>

<script>
import InforCard from '_c/info-card'
import CountTo from '_c/count-to'
import { ChartPie, ChartBar } from '_c/charts'
import Example from './example.vue'
import dayjs from 'dayjs'
import production from '@/api/production'
export default {
  name: 'home',
  components: {
    InforCard,
    CountTo,
    ChartPie,
    ChartBar,
    Example
  },
  data () {
    return {
      inforCardData: [
        { title: '新增用户', icon: 'md-person-add', count: 803, color: '#2d8cf0' },
        { title: '累计点击', icon: 'md-locate', count: 232, color: '#19be6b' },
        { title: '新增问答', icon: 'md-help-circle', count: 142, color: '#ff9900' },
        { title: '分享统计', icon: 'md-share', count: 657, color: '#ed3f14' },
        { title: '新增互动', icon: 'md-chatbubbles', count: 12, color: '#E46CBB' },
        { title: '新增页面', icon: 'md-map', count: 14, color: '#9A66E4' }
      ],
      pieData: [
        { value: 335, name: '直接访问' },
        { value: 310, name: '邮件营销' },
        { value: 234, name: '联盟广告' },
        { value: 135, name: '视频广告' },
        { value: 1548, name: '搜索引擎' }
      ],
      barData: {
        Mon: 13253,
        Tue: 34235,
        Wed: 26321,
        Thu: 12340,
        Fri: 24643,
        Sat: 1322,
        Sun: 1324
      },
      passrate: {}
    }
  },
  computed () {

  },
  mounted () {
    production.GetPassRateByDevice(
      dayjs().startOf('month').format(),
      dayjs().endOf('month').format())
      .then(res => {
        console.log(res)
        this.passrate = res.data.Data
      })
  }
}
</script>

<style lang="less">
.count-style{
  font-size: 20px;
}
</style>
