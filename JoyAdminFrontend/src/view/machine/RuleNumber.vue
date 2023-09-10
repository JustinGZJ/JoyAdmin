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
        <FormItem label="表单编码" prop="FormCode">
          <Input name="FormCode" v-model="form.FormCode"></Input>
        </FormItem>
        <FormItem label="前缀" prop="Prefix">
          <Input name="Prefix" v-model="form.Prefix"></Input>
        </FormItem>
        <FormItem label="日期规则" prop="DateRule">
<!--          <Input name="DateRule" v-model="form.DateRule"></Input>-->
            <Select name="DateRule" v-model="form.DateRule">
              <Option value="yyyyMMdd">yyyyMMdd</Option>
              <Option value="yyMMdd">yyMMdd</Option>
              <Option value="yyMMddHH">yyMMddHH</Option>
              <Option value="yyMM">yyMM</Option>
            </Select>
        </FormItem>
        <FormItem label="流水号长度" prop="SNLength">
          <Select name="SNLength" v-model="form.SNLength">
            <Option value="2">2</Option>
            <Option value="3">3</Option>
            <Option value="4">4</Option>
            <Option value="5">5</Option>
            <Option value="6">6</Option>
          </Select>
        </FormItem>
        <FormItem label="生成规则" prop="GenerativeRule">
          <Input name="GenerativeRule" v-model="form.GenerativeRule" disabled></Input>
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
import { addNumberRule, deleteNumberRule, FilterNumberRuleList, updateNumberRule } from '@/api/NumberRule'

export default {
  data () {
    return {
      columns: [{
        key: 'FormCode',
        title: '表单编码'
      },
      {
        key: 'Prefix',
        title: '前缀'
      },
      {
        key: 'DateRule',
        title: '日期规则'
      },
      {
        key: 'SNLength',
        title: '流水号长度'
      },
      {
        key: 'GenerativeRule',
        title: '生成规则'
      },
      {
        key: 'Remark',
        title: '备注'
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
        'NumberRule_Id': 0,
        'FormCode': '',
        'Prefix': '',
        'DateRule': 'yyyyMMdd',
        'SNLength': 4,
        'GenerativeRule': '',
        'Remark': '',
        'CreateDate': '2019-08-24T14:15:22Z',
        'CreateID': 0,
        'Creator': '',
        'Modifier': '',
        'ModifyDate': '2019-08-24T14:15:22Z',
        'ModifyID': 0
      },
      rules: {
        FormCode: [
          { required: true, message: '请输入表单编码', trigger: 'blur' }
        ],
        Prefix: [
          { required: true, message: '请输入前缀', trigger: 'blur' }
        ],
        DateRule: [
          { required: true, message: '请输入日期规则', trigger: 'blur' }
        ],
        SNLength: [
          { required: true, message: '请输入流水号长度', trigger: 'blur' }
        ]
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
      FilterNumberRuleList({
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
          deleteNumberRule(row.NumberRule_Id).then(res => {
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
            this.form.NumberRule_Id = 0
            this.form.CreateID = this.userId
            this.form.Creator = this.userName
            this.form.ModifyID = this.userId
            this.form.Modifier = this.userName
            this.form.CreateDate = dayjs().format()
            this.form.ModifyDate = dayjs().format()
            this.form.GenerativeRule = this.form.Prefix + dayjs().format(this.form.DateRule.toUpperCase()) + '0001'
            addNumberRule(this.form).then(res => {
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
            this.form.GenerativeRule = this.form.Prefix + dayjs().format(this.form.DateRule.toUpperCase()) + '0001'
            updateNumberRule(this.form).then(res => {
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
