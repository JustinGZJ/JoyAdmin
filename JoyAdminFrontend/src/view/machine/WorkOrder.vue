<template>
  <div>
    <div style="display:flex;justify-content: space-between">
      <div style="display: flex;align-items: center">
        <Icon size="20" type="md-apps" />
        <h3>工单管理</h3>
      </div>
      <div>
        <Button type="primary" @click="addOrder">新增</Button>
        <Button style="margin:5px" type="primary" @click="addOrder">新增</Button>
      </div>

    </div>
    <Table :columns="columns" :data="orders">
      <template #operation="{ index,row }" >
          <Button size="small"  v-if="row.Status!=='进行中'" @click="startOrder(row)" type="primary">开始</Button>
          <Button size="small" v-if="row.Status==='进行中'" @click="endOrder(row)" type="warning">结束</Button>
          <Button size="small" style="margin-left: 5px" @click="cancelOrder(row)" type="error">取消</Button>
      </template>
      <template #status="{ index,row }" >
        <Tag size="small"  v-if="row.Status==='已完成'" type="orange">{{row.Status}}</Tag>
        <Tag size="small" v-else-if="row.Status==='未开始'" type="green">{{row.Status}}</Tag>
        <Tag size="small" v-else type="blue">{{row.Status}}</Tag>
      </template>
    </Table>
    <Page :total="TotalCount" :current="CurrentPage" :page-size="PageSize" @on-change="pageChange" @on-page-size-change="pageSizeChange" show-elevator show-sizer></Page>
    <Modal v-model="modalVisible" title="确认操作" ok-text="确定" cancel-text="取消" @on-ok="confirmAction">
      {{ modalContent }}
    </Modal>
    <Modal v-model="modalAddVisible" title="添加工单"  ok-text="确定" cancel-text="取消" @on-ok="confirmAddWorkOrder">
        // 添加工单的表单
      <Form ref="formValidate" :model="formValidate" :rules="ruleValidate" label-width="120px">
        <Row gutter="10">
          <Col span="12">
            <FormItem label="工单号" prop="WorkOrderNo">
              <Input v-model="formValidate.WorkOrderNo" placeholder="请输入工单号" />
            </FormItem>
          </Col>
          <Col span="12" gutter="10">
            <FormItem label="产品编号" prop="ProductNo">
              <Input v-model="formValidate.ProductNo" placeholder="请输入产品编号" />
            </FormItem>
          </Col>
        </Row>
        <Row gutter="10">
          <Col span="12">
            <FormItem label="产品名称" prop="ProductName">
              <Input v-model="formValidate.ProductName" placeholder="请输入产品名称" />
            </FormItem>
          </Col>
          <Col span="12">
            <FormItem label="开始时间" prop="StartTime">
              <DatePicker v-model="formValidate.StartTime" type="datetime" placeholder="请选择开始时间" />
            </FormItem>
          </Col>
        </Row>
        <Row gutter="10">
          <Col span="12">
            <FormItem label="实际数量" prop="ActualQuantity">
              <InputNumber v-model="formValidate.ActualQuantity" placeholder="请输入实际数量" />
            </FormItem>
          </Col>
          <Col span="12">
            <FormItem label="计划数量" prop="PlanQuanPlanQuantitytity">
              <InputNumber v-model="formValidate.PlanQuantity" placeholder="请输入计划数量" />
            </FormItem>
          </Col>
        </Row>
        <Row gutter="10">
          <Col span="12">
            <FormItem label="完成时间" prop="FinishTime">
              <DatePicker v-model="formValidate.FinishTime" type="datetime" placeholder="请选择完成时间" />
            </FormItem>
          </Col>
          <Col span="12">
            <FormItem label="状态" prop="Status">
              <Input v-model="formValidate.Status" placeholder="请输入状态" />
            </FormItem>
          </Col>
        </Row>
      </Form>
    </Modal>
  </div>
</template>

<script>

import { GetWorkOrders, UpdateWorkOrder } from '@/api/WorkOrder'
import dayjs from 'dayjs'

export default {
  data () {
    return {
      orders: [],
      TotalCount: 1,
      TotalPages: 1,
      CurrentPage: 1,
      PageSize: 40,
      modalAddVisible: false,
      formValidate: {
        WorkOrderNo: '',
        ProductNo: '',
        ProductName: '',
        PlanQuantity: 0,
        ActualQuantity: 0,
        StartTime: '',
        FinishTime: '',
        Status: ''
      },
      ruleValidate: {
        WorkOrderNo: [
          { required: true, message: '工单号不能为空', trigger: 'blur' }
        ],
        ProductNo: [
          { required: true, message: '产品编号不能为空', trigger: 'blur' }
        ],
        ProductName: [
          { required: true, message: '产品名称不能为空', trigger: 'blur' }
        ],
        PlanQuantity: [
          { required: true, message: '计划数量不能为空', trigger: 'blur' }
        ],
        ActualQuantity: [
          { required: true, message: '实际数量不能为空', trigger: 'blur' }
        ],
        StartTime: [
          { required: true, message: '开始时间不能为空', trigger: 'blur' }
        ],
        FinishTime: [
          { required: true, message: '完成时间不能为空', trigger: 'blur' }
        ],
        Status: [
          { required: true, message: '状态不能为空', trigger: 'blur' }
        ]
      },
      columns: [
        {
          title: '工单编号',
          key: 'WorkOrderNo'
        },
        {
          title: '产品编号',
          key: 'ProductNo'
        },
        {
          title: '产品名称',
          key: 'ProductName'
        },
        {
          title: '计划数量',
          key: 'PlanQuantity'
        },
        {
          title: '实际数量',
          key: 'ActualQuantity'
        },
        {
          title: '开始时间',
          key: 'StartTime'
        },
        {
          title: '结束时间',
          key: 'FinishTime'
        },
        {
          title: '状态',
          slot: 'status'
        },
        {
          title: '创建时间',
          key: 'CreatedTime'
        },
        {
          title: '更新时间',
          key: 'UpdatedTime'
        },
        {
          title: '操作',
          slot: 'operation'
        }
      ],
      modalVisible: false,
      modalContent: '',
      currentOrder: null,
      actionType: ''
    }
  },
  mounted () {
    this.getData()
  },
  methods: {
    getData () {
      GetWorkOrders(this.CurrentPage, this.PageSize).then(res => {
        this.orders = res.data.Data.Items
        this.TotalCount = res.data.Data.TotalCount
        this.TotalPages = res.data.Data.TotalPages
      })
    },
    addOrder () {
      this.modalAddVisible = true
    },
    confirmAddWorkOrder () {
      this.modalAddVisible = false
    },
    startOrder (order) {
      console.log('start')
      console.log(order)
      this.currentOrder = order
      this.actionType = 'start'
      this.modalContent = `确定要开始工单${order.WorkOrderNo}吗？`
      this.modalVisible = true
      this.currentOrder.Status = '进行中'
    },
    endOrder (order) {
      console.log('end')
      console.log(order)
      this.currentOrder = order
      this.actionType = 'end'
      this.modalContent = `确定要结束工单${order.WorkOrderNo}吗？`
      this.modalVisible = true
    },
    cancelOrder (order) {
      console.log('cancel')
      console.log(order)
      this.currentOrder = order
      this.actionType = 'cancel'
      this.modalContent = `确定要撤回工单${order.WorkOrderNo}吗？`
      this.modalVisible = true
    },
    confirmAction () {
      this.modalVisible = false
      switch (this.actionType) {
        case 'start':
          this.currentOrder.Status = '进行中'
          this.CurrentPage.StartTime = dayjs().format('YYYY-MM-DD HH:mm:ss')
          break
        case 'end':
          this.currentOrder.Status = '已完成'
          this.CurrentPage.FinishTIme = dayjs().format('YYYY-MM-DD HH:mm:ss')
          break
        case 'cancel':
          this.currentOrder.Status = '未开始'
          break
      }
      UpdateWorkOrder(this.currentOrder).then(res => {
        console.log(res)
      }).catch(err => {
        console.log(err)
        this.$Notice.error({
          title: '错误',
          desc: err
        })
      }).finally(() => {
        this.currentOrder = null
        this.modalVisible = false
        this.CurrentPage = 1
        this.getData()
      })
    },
    pageChange (page) {
      this.CurrentPage = page
      this.getData()
    },
    pageSizeChange (pageSize) {
      this.PageSize = pageSize
      this.getData()
    }
  }
}
</script>
