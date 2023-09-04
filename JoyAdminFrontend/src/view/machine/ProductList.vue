<template>
  <div>
    <div style="display:flex;justify-content: space-between">
      <div style="display: flex;align-items: center">
        <Icon size="20" type="md-apps"/>
        <h3>产品信息</h3>
      </div>
      <div>
        <Button type="primary" @click="add">新增</Button>
      </div>
    </div>
    <Table highlight-row :columns="columns" :data="tableData"></Table>
    <div class="pagination">
      <Page
          :current="currentPage"
          :page-size="pageSize"
          :total="total"
          @on-change="handlePageChange"
          @on-page-size-change="pageSizeChange"
          show-sizer
          show-total
      ></Page>
    </div>
    <Modal width="60%" v-model="modalVisible" :title="modalTitle" ok-text="确定" cancel-text="取消" @on-ok="submitForm"
           @on-cancel="cancelModal">
      <Form ref="form" :model="form" :rules="rules">
        <Row type="flex" :gutter="10">
          <Col span="6">
            <FormItem label="产品编码" prop="ProductCode">
              <Input name="ProductCode" v-model="form.ProductCode"></Input>
            </FormItem>
          </Col>
          <Col span="6">
            <FormItem label="产品名称" prop="ProductName">
              <Input name="ProductName" v-model="form.ProductName"></Input>
            </FormItem>
          </Col>
          <Col span="6">
            <FormItem label="单位" prop="Unit_Id">
<!--              <Input name="Unit_Id" v-model="form.Unit_Id"></Input>-->
              <Select name="Unit_Id" v-model="form.Unit_Id">
                <Option v-for="item in SysUnits" :value="item.Unit_Id" :key="item.Unit_Id">
                  {{ item.UnitName }}
                </Option>
              </Select>
            </FormItem>
          </Col>
          <Col span="6">
            <FormItem label="产品规格" prop="ProductStandard">
              <Input name="ProductStandard" v-model="form.ProductStandard"></Input>
            </FormItem>
          </Col>
          <Col span="6">
            <FormItem label="产品属性" prop="ProductAttribute">
              <Input name="ProductAttribute" v-model="form.ProductAttribute"></Input>
            </FormItem>
          </Col>
          <Col span="6">
            <FormItem label="是否成品" prop="FinishedProduct">
              <Select name="FinishedProduct" v-model="form.FinishedProduct">
                <Option value="是">是</Option>
                <Option value="否">否</Option>
              </Select>
            </FormItem>
          </Col>
          <Col span="6">
            <FormItem label="工艺流程" prop="Process_Id">
              <!--           <Input name="ProcessLine_Id" v-model="form.ProcessLine_Id"></Input>-->
              <Select name="ProcessLine_Id" v-model="form.ProcessLine_Id">
                <Option v-for="item in ProcessLines" :value="item.ProcessLine_Id" :key="item.ProcessLine_Id">
                  {{ item.ProcessLineName }}
                </Option>
              </Select>
            </FormItem>
          </Col>
        </Row>
        <Row type="flex" justify="space-between" :gutter="10">
          <Col span="6">
            <FormItem label="最大库存量" prop="MaxInventory">
              <Input name="MaxInventory" v-model="form.MaxInventory"></Input>
            </FormItem>
          </Col>
          <Col span="6">
            <FormItem label="最小库存量" prop="MinInventory">
              <Input name="MinInventory" v-model="form.MinInventory"></Input>
            </FormItem>
          </Col>
          <Col span="6">
            <FormItem label="安全库存量" prop="SafeInventory">
              <Input name="SafeInventory" v-model="form.SafeInventory"></Input>
            </FormItem>
          </Col>
          <Col span="6">
            <FormItem label="当前库存量" prop="InventoryQty">
              <Input name="InventoryQty" v-model="form.InventoryQty"></Input>
            </FormItem>
          </Col>
        </Row>
      </Form>

    </Modal>
  </div>
</template>

<script>

import { addProduct, deleteProduct, FilterProductList, updateProduct } from '@/api/Product'
import dayjs from 'dayjs'
import { getProcessLineList } from '@/api/ProcessLine'
import {getSysUnitList} from "@/api/sysunit";

export default {
  data () {
    return {
      filterData: [],
      columns: [
        {
          title: '产品编码',
          key: 'ProductCode'
        },
        {
          title: '产品名称',
          key: 'ProductName'
        },
        {
          title: '单位',
          key: 'UnitName'
        },
        {
          title: '产品规格',
          key: 'ProductStandard'
        },
        {
          title: '产品属性',
          key: 'ProductAttribute'
        },
        {
          title: '工艺流程',
          key: 'ProcessLineName'
        },
        {
          title: '最大库存量',
          key: 'MaxInventory'
        },
        {
          title: '最小库存量',
          key: 'MinInventory'
        },
        {
          title: '安全库存量',
          key: 'SafeInventory'
        },
        {
          title: '当前库存量',
          key: 'InventoryQty'
        },
        {
          title: '是否成品',
          key: 'FinishedProduct'
        },
        {
          title: '创建时间',
          key: 'CreateDate'
        },
        {
          title: '创建人',
          key: 'Creator'
        },
        {
          title: '修改时间',
          key: 'ModifyDate'
        },
        {
          title: '修改人',
          key: 'Modifier'
        },
        {
          title: '操作',
          key: 'action',
          width: 150,
          render: (h, params) => {
            return h('div', [
              h(
                'Button',
                {
                  props: { type: 'primary', size: 'small' },
                  on: {
                    click: () => {
                      this.edit(params.row)
                    }
                  }
                },
                '编辑'
              ),
              h(
                'Button',
                {
                  props: { type: 'error', size: 'small' },
                  style: { marginLeft: '5px' },
                  on: {
                    click: () => {
                      this.delete(params.row)
                    }
                  }
                },
                '删除'
              )
            ])
          }
        }
      ],
      currentPage: 1,
      pageSize: 10,
      total: 0,
      filterProperty: null,
      filterValue: null,
      sortProperty: null,
      modalVisible: false,
      desc: true,
      modalTitle: '新增',
      form: {
        Product_Id: 0,
        ProductCode: '',
        ProductName: '',
        Unit_Id: 0,
        ProductStandard: '',
        ProductAttribute: '',
        ProcessLine_Id: 0,
        MaxInventory: 0,
        MinInventory: 0,
        SafeInventory: 0,
        InventoryQty: 0,
        FinishedProduct: '',
        CreateDate: '',
        CreateID: 0,
        Creator: '',
        Modifier: '',
        ModifyDate: '',
        ModifyID: 0
      },
      rules: {
        ProductCode: [{ required: true, message: '请输入产品编码', trigger: 'blur' }],
        ProductName: [{ required: true, message: '请输入产品名称', trigger: 'blur' }],
        ProductStandard: [{ required: true, message: '请输入产品规格', trigger: 'blur' }]
      },
      ProcessLines: [],
      SysUnits:[]
    }
  },
  mounted () {
    this.getProcessLines()
    this.getSysUnits()
    this.$nextTick(() => {
      this.getData()
    })
  },
  methods: {
    getData () {
      // 从后端获取数据
      FilterProductList({
        'page': this.currentPage,
        'size': this.pageSize,
        'filterProperty': this.filterProperty,
        'filterValue': this.filterValue,
        'sortProperty': this.sortProperty,
        'desc': this.desc
      }).then(res => {
        const { Succeeded, Errors, Data } = res.data
        if (Succeeded) {
          this.filterData = Data.Items
          this.total = Data.TotalCount
        } else {
          this.$Notice.error({
            title: '错误',
            desc: Errors
          })
        }
      })
    },
    add () {
      this.modalVisible = true
      this.modalTitle = '新增'
      this.$refs.form.resetFields()
    },
    edit (row) {
      // 打开编辑模态框
      this.modalVisible = true
      this.modalTitle = '编辑'
      // 将表单数据赋值为当前编辑的产品信息
      this.form = { ...row }
    },
    handlePageChange (page) {
      this.currentPage = page
      this.getData()
    },
    pageSizeChange (pageSize) {
      this.PageSize = pageSize
      this.getData()
    },
    delete (row) {
      // 删除产品
      this.$Modal.confirm({
        title: '删除',
        content: '确定删除该产品吗？',
        onOk: () => {
          deleteProduct(row.Product_Id).then(res => {
            const { Succeeded, Errors } = res.data
            if (Succeeded) {
              this.$Notice.success({
                title: '成功',
                desc: '删除成功'
              })
            } else {
              this.$Notice.error({
                title: '错误',
                desc: Errors
              })
            }
          }).finally(() => {
            this.getData()
          })
        }
      })
    },
    submitForm () {
      console.log('submit')
      // 提交表单数据
      this.$refs.form.validate(valid => {
        if (valid) {
          if (this.modalTitle === '新增') {
            this.form.Product_Id = 0
            this.form.CreateID = this.userId
            this.form.Creator = this.userName
            this.form.ModifyID = this.userId
            this.form.Modifier = this.userName
            this.form.CreateDate = dayjs().format()
            this.form.ModifyDate = dayjs().format()
            addProduct(this.form).then(res => {
              const { Succeeded, Errors } = res.data
              if (Succeeded) {
                this.$Notice.success({
                  title: '成功',
                  desc: '添加成功'
                })
              } else {
                this.$Notice.error({
                  title: '错误',
                  desc: Errors
                })
              }
            }).finally(() => {
              this.modalVisible = false
              this.currentPage = 1
              this.getData()
              this.form.resetFields()
            })
          } else {
            this.form.ModifyID = this.userId
            this.form.Modifier = this.userName
            this.form.ModifyDate = dayjs().format()
            updateProduct(this.form).then(res => {
              const { Succeeded, Errors } = res.data
              if (Succeeded) {
                this.$Notice.success({
                  title: '成功',
                  desc: '编辑成功'
                })
              } else {
                this.$Notice.error({
                  title: '错误',
                  desc: Errors
                })
              }
            }).finally(() => {
              this.modalVisible = false
              this.currentPage = 1
              this.getData()
              this.form.resetFields()
            })
          }
        }
      })
    },
    cancelModal () {
      // 取消编辑或添加操作，关闭模态框
      this.modalVisible = false
      this.$refs.form.resetFields()
    },
    getProcessLines () {
      getProcessLineList().then(res => {
        const { Succeeded, Errors, Data } = res.data
        if (Succeeded) {
          this.ProcessLines = Data
        } else {
          this.$Notice.error({
            title: '错误',
            desc: Errors
          })
        }
      })
    },
    getSysUnits(){
      getSysUnitList().then(res => {
        const { Succeeded, Errors, Data } = res.data
        if (Succeeded) {
          this.SysUnits = Data
        } else {
          this.$Notice.error({
            title: '错误',
            desc: Errors
          })
        }
      })
    }
  },
  computed: {
    userName () {
      return (
        this.$store.state.user.userName
      )
    },
    userId () {
      return (
        this.$store.state.user.userId
      )
    },
    tableData () {
      return this.filterData.map(item => {
        const data = this.ProcessLines.find(p => p.ProcessLine_Id === item.ProcessLine_Id)
        if (data) {
          item.ProcessLineName = data.ProcessLineName
        }
        const unit = this.SysUnits.find(p => p.Unit_Id === item.Unit_Id)
        if (unit) {
          item.UnitName = unit.UnitName
        }
        return item
      })
    }
  }
}
</script>

<style scoped>
.pagination {
  margin-top: 20px;
  text-align: center;
}
</style>
