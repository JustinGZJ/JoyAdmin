<template>
  <div>
    <div style="display:flex;justify-content: space-between">
      <div style="display: flex;align-items: center">
        <Icon size="20" type="md-apps"/>
        <h3>工序采集数据</h3>
      </div>
      <div>
        <Button type="primary" @click="add">新增</Button>
      </div>
    </div>
    <Table :columns="columns" :data="tableData"></Table>
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
    <Modal width="60%" v-model="modalVisible" :title="modalTitle" ok-text="确定" cancel-text="取消" @on-ok="submitForm"
           @on-cancel="cancelModal">
      <Form ref="form" :model="form" :rules="rules">
        <FormItem label="工序列表ID" prop="ProcessList_Id">
          <Input v-model="form.ProcessList_Id"/>
        </FormItem>
        <FormItem label="工序ID" prop="Process_Id">
          <Input v-model="form.Process_Id"/>
        </FormItem>
        <FormItem label="数据点类型" prop="DataPointType">
          <Input v-model="form.DataPointType"/>
        </FormItem>
        <FormItem label="数据点名称" prop="DataPointName">
          <Input v-model="form.DataPointName"/>
        </FormItem>
      </Form>
    </Modal>
  </div>
</template>

<script>

import dayjs from 'dayjs'
import { addProcessList, deleteProcessList, FilterProcessListList, updateProcessList } from '@/api/ProcessList'

export default {
  data () {
    return {
      tableData: [],
      columns: [
        {
          key: 'ProcessList_Id',
          title: '工序列表ID'
        },
        {
          key: 'Process_Id',
          title: '工序ID'
        },
        {
          key: 'DataPointType',
          title: '数据点类型'
        },
        {
          key: 'DataPointName',
          title: '数据点名称'
        },
        {
          key: 'CreateDate',
          title: '创建日期'
        },
        {
          key: 'Creator',
          title: '创建者'
        },
        {
          key: 'Modifier',
          title: '修改者'
        },
        {
          key: 'ModifyDate',
          title: '修改日期'
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
        'ProcessList_Id': 0,
        'Process_Id': 0,
        'DataPointType': 'string',
        'DataPointName': 'string',
        'CreateDate': '2023-08-25T01:19:16.843Z',
        'CreateID': 0,
        'Creator': 'string',
        'Modifier': 'string',
        'ModifyDate': '2023-08-25T01:19:16.843Z',
        'ModifyID': 0
      },
      rules: {
        'ProcessList_Id': [
          { required: true, message: '请输入工序列表ID', trigger: 'blur' }
        ],
        'Process_Id': [
          { required: true, message: '请输入工序ID', trigger: 'blur' }
        ],
        'DataPointType': [
          { required: true, message: '请输入数据点类型', trigger: 'blur' }
        ],
        'DataPointName': [
          { required: true, message: '请输入数据点名称', trigger: 'blur' }
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
      FilterProcessListList({
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
          deleteProcessList(row.Product_Id).then(res => {
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
            addProcessList(this.form).then(res => {
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
            updateProcessList(this.form).then(res => {
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
