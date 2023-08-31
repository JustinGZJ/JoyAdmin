<template>
  <div>
    <div style="display:flex;justify-content: space-between">
      <div style="display: flex;align-items: center">
        <Icon size="20" type="md-apps"/>
        <h3>工艺路线-工序</h3>
      </div>
      <div>
        <Button v-if="ProcessLine_Id!==0" type="primary" @click="add">新增</Button>
      </div>
    </div>
    <Table :columns="columns" :data="tableData" draggable @on-drag-drop="changeOrder"></Table>
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
        <FormItem label="工艺流程类型" prop="ProcessLineType">
          <Select v-model="form.ProcessLineType">
            <Option value="工序">工序</Option>
            <Option value="工艺路线">工艺路线</Option>
          </Select>
        </FormItem>
        <FormItem label="工艺" prop="Process_Id">
          <Select v-model="form.Process_Id">
            <Option v-for="item in Processes" :value="item.Process_Id" :key="item.Process_Id">{{ item.ProcessName }}
            </Option>
          </Select>
        </FormItem>
        <FormItem label="工艺流程" prop="ProcessLineDown_Id">
          <Select v-model="form.ProcessLineDown_Id">
            <Option v-for="item in ProcessLines" :value="item.ProcessLine_Id" :key="item.ProcessLine_Id">
              {{ item.ProcessLineName }}
            </Option>
          </Select>
        </FormItem>
        <FormItem label="顺序" prop="Sequence">
          <Input v-model="form.Sequence"/>
        </FormItem>
        <FormItem label="报工比例" prop="SubmitWorkMatch">
          <Input v-model="form.SubmitWorkMatch"/>
        </FormItem>
      </Form>
    </Modal>
  </div>
</template>

<script>

import dayjs from 'dayjs'
import {
  addProcessLineList, batchUpdateProcessLineList,
  FilterProcessLineListList,
  updateProcessLineList
} from '@/api/ProcessLineList'
import { getProcessList } from '@/api/Process'
import { getProcessLineList } from '@/api/ProcessLine'

export default {
  props: {
    ProcessLine_Id: {
      type: Number,
      default: 0
    }
  },
  data () {
    return {
      tableData: [],
      columns: [
        {
          type: 'selection',
          width: 60,
          align: 'center'
        },
        {
          type: 'index',
          width: 60,
          align: 'center'
        },
        {
          title: '工艺流程',
          key: 'ProcessLineName'
        },
        {
          title: '工艺流程类型',
          key: 'ProcessLineType'
        },
        {
          title: '工艺',
          key: 'ProcessName'
        },
        {
          title: '下行工艺流程',
          key: 'ProcessLineDownName'
        },
        {
          title: '顺序',
          key: 'Sequence',
          sortable: true, // 开启排序
          sortType: 'asc'// 初始化排序
        },
        {
          title: '报工比例',
          key: 'SubmitWorkMatch'
        },
        {
          title: '创建日期',
          key: 'CreateDate'
        },
        {
          title: '创建者',
          key: 'Creator'
        },
        {
          title: '修改者',
          key: 'Modifier'
        },
        {
          title: '修改日期',
          key: 'ModifyDate'
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
      pageSize: 40,
      total: 0,
      filterProperty: null,
      filterValue: null,
      sortProperty: null,
      modalVisible: false,
      desc: true,
      modalTitle: '新增',
      form: {
        'ProcessLineList_Id': 0,
        'ProcessLine_Id': 0,
        'ProcessLineType': '工序',
        'Process_Id': 0,
        'ProcessLineDown_Id': 0,
        'Sequence': 0,
        'SubmitWorkMatch': 0,
        'CreateDate': '2023-08-23T08:45:43.020Z',
        'CreateID': 0,
        'Creator': 'string',
        'Modifier': 'string',
        'ModifyDate': '2023-08-23T08:45:43.020Z',
        'ModifyID': 0
      },
      rules: {
        ProcessLineType: [
          { required: true, message: '请输入工艺流程类型', trigger: 'blur' }
        ]
      },
      Processes: [],
      ProcessLines: []
    }
  },
  mounted () {
    this.getData()
    this.getProcessLines()
    this.getProcesses()
  },
  methods: {
    getData () {
      // 从后端获取数据
      FilterProcessLineListList({
        'page': this.currentPage,
        'size': this.pageSize,
        'filterProperty': this.filterProperty,
        'filterValue': this.filterValue,
        'sortProperty': this.sortProperty,
        'desc': this.desc
      }).then(res => {
        this.tableData = res.data.Data.Items.map(item => {
          //  item.ProcessLineType = item.ProcessLineType === 0 ? '工序' : '工艺路线'
          item.ProcessLineName = item.ProcessLine_Id === 0 ? '' : this.ProcessLines.find(x => x.ProcessLine_Id === item.ProcessLine_Id).ProcessLineName
          item.ProcessName = item.Process_Id === 0 ? '' : this.Processes.find(x => x.Process_Id === item.Process_Id).ProcessName
          item.ProcessLineDownName = item.ProcessLineDown_Id === 0 ? '' : this.ProcessLines.find(x => x.ProcessLine_Id === item.ProcessLineDown_Id).ProcessLineName
          return item
        })
        this.total = res.data.Data.TotalCount
      })
    },
    add () {
      this.modalVisible = true
      this.modalTitle = '新增'
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
          addProcessLineList(row.Product_Id).then(res => {
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
      console.log(this.form)
      // 提交表单数据
      this.$refs.form.validate(valid => {
        if (valid) {
          if (this.modalTitle === '新增') {
            this.form.ProcessLineList_Id = 0
            this.form.ProcessLine_Id = this.ProcessLine_Id
            this.form.CreateID = this.userId
            this.form.ModifyDate = dayjs().format()
            this.form.Creator = this.userName
            this.form.CreateDate = dayjs().format()

            addProcessLineList(this.form).then(res => {
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
            })
          } else {
            this.form.ModifyID = this.userId
            this.form.Modifier = this.userName
            this.form.ModifyDate = dayjs().format()
            updateProcessLineList(this.form).then(res => {
              const { Succeeded, Errors } = res.data

              if (Succeeded) {
                this.$Notice.success({
                  title: '成功',
                  desc: '修改成功'
                })
              } else {
                this.$Notice.error({
                  title: '错误',
                  desc: Errors
                })
              }
              //  this.$refs.form.resetFields()
            }).finally(() => {
              this.modalVisible = false
              this.currentPage = 1
              this.getData()
            })
          }
        } else {
          console.error('error submit!!', this.form)
          this.$Notice.error({
            title: '错误',
            desc: '表单验证失败' + JSON.stringify(this.form)
          })
        }
      })
    },
    cancelModal () {
      // 取消编辑或添加操作，关闭模态框
      this.modalVisible = false
      this.$refs.form.resetFields()
    },
    getProcesses () {
      getProcessList().then(res => {
        const { Succeeded, Errors, Data } = res.data
        if (Succeeded) {
          this.Processes = Data
        } else {
          this.$Notice.error({
            title: '错误',
            desc: Errors
          })
        }
      })
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
    changeOrder (oldIndex, newIndex) {
      // [this.tableData[oldIndex], this.tableData[newIndex]] = [this.tableData[newIndex], this.tableData[oldIndex]]

      const oldItem = this.tableData[oldIndex]
      const newItem = this.tableData[newIndex]
      const oldSequence = oldItem.Sequence
      oldItem.Sequence = newItem.Sequence
      newItem.Sequence = oldSequence
      batchUpdateProcessLineList([newItem, oldItem]).then(res => {
        const { Succeeded, Errors } = res.data
        if (Succeeded) {
          this.$Notice.success({
            title: '成功',
            desc: '修改成功'
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
  },
  watch: {
    ProcessLine_Id (newValue, oldValue) {
      this.filterProperty = 'ProcessLine_Id'
      this.filterValue = newValue
      this.getData()
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
