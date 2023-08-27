<template>
  <div>
    <div>
      <div style="display:flex;justify-content: space-between">
        <div style="display: flex;align-items: center">
          <Icon size="20" type="md-apps"/>
          <h3>工序</h3>
        </div>
        <div>
          <Button type="primary" @click="add">新增</Button>
        </div>
      </div>
      <Table highlight-row=true  :columns="columns" :data="tableData" @on-current-change="handleCurrentRowChange"></Table>
      <div class="pagination">
        <Page
            :current="currentPage"
            :page-size="pageSize"
            :total="total"
            @on-change="handlePageChange"
            @on-page-size-change="pageSizeChange"
            show-sizer="true"
            show-total="true"
        ></Page>
      </div>
      <Modal width="60%" v-model="modalVisible" :title="modalTitle" ok-text="确定" cancel-text="取消"  @on-ok="submitForm" @on-cancel="cancelModal">
        <Form ref="form" :model="form" :rules="rules">
          <FormItem label="工序编码" prop="ProcessCode">
            <Input v-model="form.ProcessCode"></Input>
          </FormItem>
          <FormItem label="工序名称" prop="ProcessName">
            <Input v-model="form.ProcessName"></Input>
          </FormItem>
          <FormItem label="报工权限" prop="SubmitWorkLimit">
            <Input v-model="form.SubmitWorkLimit"></Input>
          </FormItem>
          <FormItem label="报工配比" prop="SubmitWorkMatch">
            <Input v-model="form.SubmitWorkMatch"></Input>
          </FormItem>
          <FormItem label="缺陷项目" prop="DefectItem">
            <Input v-model="form.DefectItem"></Input>
          </FormItem>
        </Form>

      </Modal>
    </div>
    <ProcessList :Process_Id="selectedRow?selectedRow.Process_Id:0"></ProcessList>
  </div>
</template>

<script>

import dayjs from 'dayjs'
import { addProcess, deleteProcess, FilterProcessList, updateProcess } from '@/api/Process'

export default {
  data () {
    return {
      tableData: [],
      columns: [
        {
          title: '工序编码',
          key: 'ProcessCode'
        },
        {
          title: '工序名称',
          key: 'ProcessName'
        },
        {
          title: '报工权限',
          key: 'SubmitWorkLimit'
        },
        {
          title: '报工配比',
          key: 'SubmitWorkMatch'
        },
        {
          title: '缺陷项目',
          key: 'DefectItem'
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
              h('Button', {
                props: {
                  type: 'info',
                  size: 'small'
                },
                style: {
                  marginRight: '5px'
                },
                on: {
                  click: () => {
                    this.edit(params.row)
                  }
                }
              }, '编辑'),
              h('Button', {
                props: {
                  type: 'error',
                  size: 'small'
                },
                on: {
                  click: () => {
                    this.delete(params.row)
                  }
                }
              }, '删除')
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
        'Process_Id': 0,
        'ProcessCode': '',
        'ProcessName': '',
        'SubmitWorkLimit': '',
        'SubmitWorkMatch': 0,
        'DefectItem': '',
        'CreateDate': '2023-08-25T01:19:16.843Z',
        'CreateID': 0,
        'Creator': 'string',
        'Modifier': 'string',
        'ModifyDate': '2023-08-25T01:19:16.843Z',
        'ModifyID': 0
      },
      rules: {
        'ProcessCode': [
          { required: true, message: '请输入工序编码', trigger: 'blur' }
        ],
        'ProcessName': [
          { required: true, message: '请输入工序名称', trigger: 'blur' }
        ],
        'SubmitWorkLimit': [
          { required: true, message: '请输入报工权限', trigger: 'blur' }
        ],
        'SubmitWorkMatch': [
          { required: true, message: '请输入交工配比', trigger: 'blur' }
        ],
        'DefectItem': [
          { required: true, message: '请输入缺陷项目', trigger: 'blur' }
        ]
      },
      selectedRow: null
    }
  },
  mounted () {
    this.getData()
  },
  methods: {
    getData () {
      // 从后端获取数据
      FilterProcessList({
        'page': this.currentPage,
        'size': this.pageSize,
        'filterProperty': this.filterProperty,
        'filterValue': this.filterValue,
        'sortProperty': this.sortProperty,
        'desc': this.desc
      }).then(res => {
        this.tableData = res.data.Data.Items
        this.total = res.data.Data.TotalCount
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
          deleteProcess(row.Product_Id).then(res => {
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
            addProcess(this.form).then(res => {
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
            updateProcess(this.form).then(res => {
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
    handleCurrentRowChange (row) {
      this.selectedRow = row
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
  },
  components: {
    ProcessList: () => import('@/view/machine/ProcessList.vue')
  }
}
</script>

<style scoped>
.pagination {
  margin-top: 20px;
  text-align: center;
}
</style>
