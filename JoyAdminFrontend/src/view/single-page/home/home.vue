<template>
  <div>
<!--   // 工单信息-->
    <h3>工单信息</h3>
    <Row :gutter="10">
      <i-col :xs="24" :md="12" :lg="6" v-for="(item, index) in OrderInfos" :key="`infor-${index}`"
             style="height: 120px;padding-bottom: 10px;">
        <InforCard :left="30" :color="index % 2 === 0 ? '#2d8cf0' : '#19be6b'" :icon="index % 2 === 0 ? 'md-outlet' : 'ios-pulse-outline'">

          <div slot>
            <div>{{ item.title }}</div>
            <div class="count-style">{{item.value}}</div>
          </div>
        </InforCard>
      </i-col>
    </Row>
    <h3>设备合格率</h3>
    <Row :gutter="10">
      <i-col :xs="12" :md="8" :lg="4" v-for="(pr, i) in passrate" :key="`infor-${i}`"
             style="height: 120px;padding-bottom: 10px;">
        <card shadow style="padding: 0" padding="0">
          <div
              style="padding:2px 5px; white-space: nowrap;background: rgb(85, 206, 128);font-size: 20px;font-weight: bold;color: white">
            {{ pr.Device }}
          </div>
          <div style="padding: 5px">
            <div style="font-size: 14px">
              <span>合格数:</span>
              <span>{{ pr.Ok }}</span>
            </div>
            <div style="font-size: 14px">
              <span>不合格:</span>
              <span>{{ pr.Ng }}</span>
            </div>
            <div style="font-size: 14px">
              <span>合格率:</span>
              <span>{{ pr.OkRate }}</span>
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
import production from '@/api/production'
import { FilterWorkOrderList } from '@/api/WorkOrder'
import dayjs from 'dayjs'

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
      passrate: {},
      orders: [],
      OrderInfos: []
    }
  },
  computed () {

  },
  mounted () {
    this.getActiveWorkOrder()
    setInterval(() => {
      this.getActiveWorkOrder()
    }, 1000 * 5)
  },
  methods: {
    getActiveWorkOrder () {
      FilterWorkOrderList({
        desc: true,
        filterProperty: 'Status',
        filterValue: '进行中',
        page: 1,
        size: 40,
        sortProperty: 'UpdatedTime'
      }).then(res => {
        this.orders = res.data.Data.Items
        if (this.orders.length > 0) {
          production.GetPassRateByWorkOrder(this.orders[0].WorkOrderNo).then(res => {
            console.log(res)
            this.passrate = res.data.Data
          })

          let order = this.orders[0]
          this.OrderInfos = [
            {
              title: '工单号',
              value: order.WorkOrderNo
            },
            {
              title: '产品名称',
              value: order.ProductName
            },
            {
              title: '预设数量',
              value: order.PlanQuantity + '个'
            },
            {
              title: '出料数量',
              value: order.ActualQuantity + '个'
            },
            {
              title: 'NG数量',
              value: order.NgQuantity + '个'
            },
            {
              title: '合格率',
              value: order.ActualQuantity * 100 / (order.ActualQuantity + order.NgQuantity) + '%'
            },
            {
              title: '完成率',
              value: order.ActualQuantity * 100 / order.PlanQuantity + '%'
            },
            {
              title: '剩余时间',
              value: dayjs(order.FinishTime).diff(dayjs(), 'hour') + '小时'
            }]
        }
      })
    }
  }
}
</script>

<style lang="less">
.count-style {
  font-size: 20px;
}
</style>
