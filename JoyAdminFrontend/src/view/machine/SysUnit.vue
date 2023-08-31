<template>
  <div>
    <div style="display:flex;justify-content: space-between">
      <div style="display: flex;align-items: center">
        <Icon size="20" type="md-apps"/>
        <h3>单位管理</h3>
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
        <FormItem label="单位名称" prop="UnitName">
          <Input name="UnitName" v-model="form.UnitName"></Input>
        </FormItem>
        <FormItem label="备注" prop="Remark">
          <Input name="Remark" v-model="form.Remark"></Input>
        </FormItem>
      </Form>
    </Modal>
  </div>
</template>

<script>

import dayjs from 'dayjs'
import { addSysUnit, deleteSysUnit, FilterSysUnitList, updateSysUnit } from '@/api/sysunit'

export default {
  data () {
    return {
      columns: [
        {
          title: '单位名称',
          key: 'UnitName'
        },
        {
          title: '备注',
          key: 'Remark'
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
      tableData: [],
      filterValue: null,
      sortProperty: null,
      modalVisible: false,
      desc: true,
      modalTitle: '新增',
      form: {
        'Unit_Id': 0,
        'UnitName': '',
        'Remark': '',
        'CreateDate': '2023-08-31T13:43:55.851Z',
        'CreateID': 0,
        'Creator': '',
        'Modifier': '',
        'ModifyDate': '2023-08-31T13:43:55.852Z',
        'ModifyID': 0
      },
      rules: {
        UnitName: [{ required: true, message: '请输入单位名称', trigger: 'blur' }]
      },
      ProcessLines: []
    }
  },
  mounted () {
    this.getData()
  },
  methods: {
    getData () {
      // 从后端获取数据
      FilterSysUnitList({
        'page': this.currentPage,
        'size': this.pageSize,
        'filterProperty': this.filterProperty,
        'filterValue': this.filterValue,
        'sortProperty': this.sortProperty,
        'desc': this.desc
      }).then(res => {
        const { Succeeded, Errors, Data } = res.data
        if (Succeeded) {
          this.tableData = Data.Items
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
          deleteSysUnit(row.Unit_Id).then(res => {
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
            addSysUnit(this.form).then(res => {
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
            updateSysUnit(this.form).then(res => {
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
