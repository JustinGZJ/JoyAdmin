<template>
  <div>
    <div style="display:flex;justify-content: space-between;margin-bottom: 10px">
      <div style="display: flex;align-items: center">
        <Icon size="20" type="md-apps"/>
        <h3>工单管理</h3>
        <div style="margin-left: 30px">
          <RadioGroup v-model="filterValue" type="button" button-style="solid" @on-change="getData">
            <Radio label="进行中"></Radio>
            <Radio label="未开始"></Radio>
            <Radio label="已撤回"></Radio>
            <Radio label="已完成"></Radio>
          </RadioGroup>
        </div>
      </div>
      <div >
        <button-group>
          <Button v-if="selectedRow" type="success" @click="editOrder(selectedRow)">编辑</Button>
          <Button v-if="selectedRow" type="error" @click="deleteOrder(selectedRow)">删除</Button>
          <Button type="primary" @click="addOrder">新增</Button>
        </button-group>
      </div>
    </div>
    <Table :columns="columns" :data="orders" highlight-row @on-current-change="selectionChange">
      <template #operation="{ index,row }">
        <div>
          <Button
              v-for="btn in handleButtons(row)"
              :key="btn.text"
              :type="btn.type"
              size="small"
              @click="btn.handler(row)"
          >
            {{ btn.text }}
          </Button>
        </div>
      </template>
      <template #status="{ index,row }">
        <Tag
            size="small"
            :type="statusType(row.Status)"
        >
          {{ row.Status }}
        </Tag>
      </template>
    </Table>
    <Page :total="TotalCount" :current="CurrentPage" :page-size="PageSize" @on-change="pageChange"
          @on-page-size-change="pageSizeChange" show-elevator show-sizer></Page>
    <Modal v-model="modalVisible" title="确认操作" ok-text="确定" cancel-text="取消" @on-ok="confirmAction">
      {{ modalContent }}
    </Modal>
    <Modal v-model="modalAddVisible" :title=modalTitle ok-text="确定" cancel-text="取消" @on-ok="confirmAddWorkOrder">
      <Form ref="formValidate" :model="formValidate" :rules="ruleValidate" label-width="120px">
        <Row gutter=10>
          <Col span=12>
            <FormItem label="工单号" prop="WorkOrderNo">
              <Input v-model="formValidate.WorkOrderNo" placeholder="请输入工单号"/>
            </FormItem>
          </Col>
          <Col span=12 gutter="10">
            <FormItem label="产品编号" prop="ProductNo">
              <Select v-model="formValidate.ProductNo" placeholder="请选择产品编号"
                      @on-change="(value)=>updateFormProduct('ProductCode',value)">
                <Option v-for="product in Products" :key="product.Id" :value="product.ProductCode">
                  {{ product.ProductCode }}
                </Option>
              </Select>
            </FormItem>
          </Col>
        </Row>
        <Row gutter=10>
          <Col span=12>
            <FormItem label="产品名称" prop="ProductName">
              <Select v-model="formValidate.ProductName" placeholder="请选择产品名称"
                      @on-change="(value)=>updateFormProduct('ProductName',value)">
                <Option v-for="product in Products" :key="product.Id" :value="product.ProductName">
                  {{ product.ProductName }}
                </Option>
              </Select>
            </FormItem>
          </Col>
          <Col span=12>
            <FormItem label="开始时间" prop="StartTime">
              <DatePicker v-model="formValidate.StartTime" type="datetime" placeholder="请选择开始时间"/>
            </FormItem>
          </Col>
        </Row>
        <Row gutter=10>
          <Col span=12>
            <FormItem label="计划数量" prop="PlanQuanPlanQuantitytity">
              <InputNumber v-model="formValidate.PlanQuantity" placeholder="请输入计划数量"/>
            </FormItem>
          </Col>
          <Col span=12>
            <FormItem label="实际数量" prop="ActualQuantity">
              <InputNumber v-model="formValidate.ActualQuantity" placeholder="请输入实际数量"/>
            </FormItem>
          </Col>
        </Row>
        <Row gutter=10>
          <Col span=12>
            <FormItem label="完成时间" prop="FinishTime">
              <DatePicker v-model="formValidate.FinishTime" type="datetime" placeholder="请选择完成时间"/>
            </FormItem>
          </Col>
          <Col span=12>
            <FormItem label="状态" prop="Status">
              <!--              <Input v-model="formValidate.Status" placeholder="请输入状态"/>-->
              <Select v-model="formValidate.Status">
                <Option value="未开始">未开始</Option>
                <Option value="进行中">进行中</Option>
                <Option value="已完成">已完成</Option>
                <Option value="已撤回">已撤回</Option>
              </Select>
            </FormItem>
          </Col>
        </Row>
      </Form>
    </Modal>
  </div>
</template>

<script>

import dayjs from 'dayjs'
import { addWorkOrder, deleteWorkOrder, FilterWorkOrderList, updateWorkOrder } from '@/api/WorkOrder'
import { getProductList } from '@/api/Product'
import edit from '_c/tables/edit.vue'
import { GetNextSn } from '@/api/NumberRule'

export default {
  data () {
    return {
      orders: [],
      TotalCount: 1,
      TotalPages: 1,
      CurrentPage: 1,
      PageSize: 40,
      modalAddVisible: false,
      modalTitle: '添加工单',
      formValidate: {
        WorkOrderNo: '',
        ProductNo: '',
        ProductName: '',
        PlanQuantity: 1000,
        ActualQuantity: 0,
        NgQuantity: 0,
        StartTime: dayjs().format('YYYY-MM-DD HH:mm:ss'),
        FinishTime: dayjs().add(1, 'day').format('YYYY-MM-DD HH:mm:ss'),
        Status: '未开始'
      },
      ruleValidate: {
        ProductNo: [
          { required: true, message: '产品编号不能为空', trigger: 'blur' }
        ],
        ProductName: [
          { required: true, message: '产品名称不能为空', trigger: 'blur' }
        ],
        PlanQuantity: [
          { required: true, message: '计划数量不能为空', trigger: 'blur' }
        ]
      },
      columns: [
        {
          title: '#',
          type: 'index'
        },
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
          title: '不合格数',
          key: 'NgQuantity'
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
          key: 'UpdatedTime',
          sortable: true,
          sortType: 'desc'
        },
        {
          title: '操作',
          slot: 'operation'
        }
      ],
      modalVisible: false,
      modalContent: '',
      currentOrder: null,
      actionType: '',
      Products: [],
      selectedRow: null,
      filterValue: '进行中'
    }
  },
  computed: {
    edit () {
      return edit
    }
  },
  mounted () {
    this.getProducts()
    this.getData()
  },
  methods: {
    getData () {
      FilterWorkOrderList({
        'page': this.CurrentPage,
        'size': this.PageSize,
        'filterProperty': 'Status',
        'filterValue': this.filterValue,
        'sortProperty': 'UpdatedTime',
        'desc': true
      }).then(res => {
        this.orders = res.data.Data.Items
        this.TotalCount = res.data.Data.TotalCount
        this.TotalPages = res.data.Data.TotalPages
      })
    },
    addOrder () {
      this.modalAddVisible = true
      this.modalTitle = '添加工单'
      this.$refs.formValidate.resetFields()
    },
    editOrder (row) {
      this.modalAddVisible = true
      this.modalTitle = '编辑工单'
      this.formValidate = { ...row }
    },
    deleteOrder (row) {
      // 提示是否删除
      this.$Modal.confirm({
        title: '确认删除',
        content: '确定要删除该工单吗？',
        onOk: () => {
          // 删除
          deleteWorkOrder(row.Id).then(res => {
            const { Data, Succeeded } = res.data
            if (Succeeded) {
              this.$Notice.success({
                title: '成功',
                desc: '删除成功'
              })
            } else {
              this.$Notice.error({
                title: '错误',
                desc: JSON.stringify(Data)
              })
            }
            this.getData()
          }).catch(err => {
            console.log(err)
            this.$Notice.error({
              title: '错误',
              desc: err
            })
          }).finally(() => {
            this.CurrentPage = 1
          })
          console.log(row)
        }
      })
    },
    async confirmAddWorkOrder () {
      this.modalAddVisible = false
      if (this.modalTitle === '编辑工单') {
        updateWorkOrder(this.formValidate).then(res => {
          const { Data, Succeeded } = res.data
          if (Succeeded) {
            this.$Notice.success({
              title: '成功',
              desc: '编辑成功'
            })
          } else {
            this.$Notice.error({
              title: '错误',
              desc: JSON.stringify(Data)
            })
          }
          this.getData()
        }).catch(err => {
          console.log(err)
          this.$Notice.error({
            title: '错误',
            desc: err
          })
        }).finally(() => {
          this.modalAddVisible = false
          this.CurrentPage = 1
        })
        return
      }
      if (this.modalTitle === '添加工单') {
        this.formValidate.CreatedTime = dayjs().format('YYYY-MM-DD HH:mm:ss')
        this.formValidate.UpdatedTime = dayjs().format('YYYY-MM-DD HH:mm:ss')
        // 如果工单号为空，则自动生成工单号
        if (this.formValidate.WorkOrderNo === '') {
          this.formValidate.WorkOrderNo = (await GetNextSn('WO')).data.Data
          console.log(this.formValidate.WorkOrderNo)
          //  this.formValidate.WorkOrderNo = await GetNextSn('WO')
        }
        addWorkOrder(this.formValidate).then(res => {
          const { Data, Succeeded } = res.data
          if (Succeeded) {
            this.$Notice.success({
              title: '成功',
              desc: '添加成功'
            })
          } else {
            this.$Notice.error({
              title: '错误',
              desc: JSON.stringify(Data)
            })
          }
          this.getData()
        }).catch(err => {
          console.log(err)
          this.$Notice.error({
            title: '错误',
            desc: err
          })
        }).finally(() => {
          this.modalAddVisible = false
          this.CurrentPage = 1
        })
      }
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
          this.currentOrder.StartTime = dayjs().format('YYYY-MM-DD HH:mm:ss')
          break
        case 'end':
          this.currentOrder.Status = '已完成'
          this.currentOrder.FinishTIme = dayjs().format('YYYY-MM-DD HH:mm:ss')
          break
        case 'cancel':
          this.currentOrder.Status = '已撤回'
          break
      }
      this.currentOrder.UpdatedTime = dayjs().format('YYYY-MM-DD HH:mm:ss')
      updateWorkOrder(this.currentOrder).then(res => {
        const { Data, Succeeded } = res.data
        if (Succeeded) {
          this.$Notice.success({
            title: '成功',
            desc: '操作成功'
          })
          this.getData()
        } else {
          this.$Notice.error({
            title: '错误',
            desc: JSON.stringify(Data)
          })
        }
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
      })
    },
    pageChange (page) {
      this.CurrentPage = page
      this.getData()
    },
    pageSizeChange (pageSize) {
      this.PageSize = pageSize
      this.getData()
    },
    statusType (status) {
      if (status === '已完成') return 'orange'
      if (status === '未开始') return 'green'
      if (status === '已撤回') return 'red'
      return 'blue'
    },
    handleButtons (row) {
      return [
        {
          text: row.Status !== '进行中' ? '开始' : '结束',
          type: row.Status !== '进行中' ? 'primary' : 'warning',
          handler: row.Status !== '进行中' ? this.startOrder : this.endOrder
        },
        {
          text: '撤回',
          type: 'error',
          handler: this.cancelOrder
        }
      ]
    },
    getProducts () {
      getProductList().then(res => {
        const { Succeeded, Errors, Data } = res.data
        if (!Succeeded) {
          this.$Notice.error({
            title: '错误',
            desc: Errors
          })
        }
        this.Products = Data
      })
    },
    updateFormProduct (type, value) {
      switch (type) {
        case 'ProductCode':
          //  this.formValidate.ProductCode = value
          this.formValidate.ProductName = this.Products.find(item => item.ProductCode === value).ProductName
          break
        case 'ProductName':
          // this.formValidate.ProductName = value
          this.formValidate.ProductNo = this.Products.find(item => item.ProductName === value).ProductCode
          break
      }
    },
    selectionChange (row) {
      this.selectedRow = row
    }
  }
}
</script>
