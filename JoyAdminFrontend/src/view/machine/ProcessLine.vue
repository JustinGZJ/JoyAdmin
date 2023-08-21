<template>
  <div>
    <div>
      <div>
        <div style="display:flex;justify-content: space-between">
          <div style="display: flex;align-items: center" >
            <Icon size="20" type="md-apps"/>
            <h3>工艺路线</h3>
          </div>
          <div>
            <Button type="primary" @click="add">新增</Button>
          </div>
        </div>
        <Table highlight-row=true :columns="columns" :data="tableData" @on-current-change="handleCurrentRowChange"></Table>
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
      </div>
      <Modal width="60%" v-model="modalVisible" :title="modalTitle" ok-text="确定" cancel-text="取消"
             @on-ok="submitForm"
             @on-cancel="cancelModal">
        <Form ref="form" :model="form" :rules="rules">
          <FormItem label="流程线编码" prop="ProcessLineCode">
            <Input v-model="form.ProcessLineCode"/>
          </FormItem>
          <FormItem label="流程线名称" prop="ProcessLineName">
            <Input v-model="form.ProcessLineName"/>
          </FormItem>
        </Form>

      </Modal>
    </div>
    <ProductLineList></ProductLineList>
  </div>
</template>

<script>

import dayjs from 'dayjs'
import { addProcessLine, deleteProcessLine, FilterProcessLineList, updateProcessLine } from '@/api/ProcessLine'
import { FilterProcessLineListList } from '@/api/ProcessLineList'

export default {
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
          title: '流程线编码',
          key: 'ProcessLineCode'
        },
        {
          title: '流程线名称',
          key: 'ProcessLineName'
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
      pageSize: 10,
      total: 0,
      filterProperty: null,
      filterValue: null,
      sortProperty: null,
      desc: false,
      modalVisible: false,
      modalTitle: '新增',
      form: {
        ProcessLine_Id: 0,
        ProcessLineCode: '',
        ProcessLineName: '',
        CreateDate: '',
        CreateID: 0,
        Creator: '',
        Modifier: '',
        ModifyDate: '',
        ModifyID: 0
      },
      rules: {
        ProcessLineCode: [
          { required: true, message: '请输入流程线编码', trigger: 'blur' }
        ],
        ProcessLineName: [
          { required: true, message: '请输入流程线名称', trigger: 'blur' }
        ]
      }
    }
  },
  mounted () {
    this.getData()
  },
  methods: {
    getData () {
      // 从后端获取数据
      FilterProcessLineList({
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
    },
    edit (row) {
      // 打开编辑模态框
      this.modalVisible = true
      this.modalTitle = '编辑'
      // 将表单数据赋值为当前编辑的产品信息
      this.form = { ...row }
    },
    handleCurrentRowChange (row) {
      // 获取当前行的工艺流程ID
      const { ProcessLine_Id } = row
      // 根据工艺流程ID获取工序列表
      FilterProcessLineListList({
        'page': this.currentPage,
        'size': this.pageSize,
        'filterProperty': 'ProcessLine_Id',
        'filterValue': ProcessLine_Id,
        'sortProperty': this.sortProperty,
        'desc': this.desc
      }).then(res => {
        this.tableData = res.data.Data.Items
        this.total = res.data.Data.TotalCount
      })
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
          deleteProcessLine(row.ProcessLine_Id).then(res => {
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
      // 提交表单数据
      this.$refs.form.validate(valid => {
        if (valid) {
          if (this.modalTitle === '新增') {
            this.form.ProcessLine_Id = 0
            this.form.CreateID = this.userId
            this.form.Creator = this.userName
            this.form.ModifyID = this.userId
            this.form.Modifier = this.userName
            this.form.CreateDate = dayjs().format()
            this.form.ModifyDate = dayjs().format()
            addProcessLine(this.form).then(res => {
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
            updateProcessLine(this.form).then(res => {
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
    ProductLineList: () => import('@/view/machine/ProcessLineList.vue')
  }
}
</script>

<style scoped>
.pagination {
  margin-top: 20px;
  text-align: center;
}
</style>
