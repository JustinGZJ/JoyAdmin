<template>
  <div>
    <Card style="margin-bottom: 10px">
      <Button type="primary" @click="AddData" icon="md-add">新增</Button>
    </Card>
    <Table
      ref="table"
      border
      :loading="loadingTable"
      :columns="tableTitle"
      :data="tableData"
    ></Table>
    <Modal
      :width="400"
      v-model="modalData"
      :title="tempData.Id > 0 ? '修改权限' : '新增权限'"
      :mask-closable="false"
    >
      <Form
        ref="formValidate"
        :model="tempData"
        :label-width="80"
        :rules="ruleValidate"
      >
        <FormItem label="权限名称" prop="UniqueName">
          <Input v-model="tempData.UniqueName" />
        </FormItem>
        <FormItem label="唯一编码" prop="UniqueCode">
          <Input v-model="tempData.UniqueCode" />
        </FormItem>
         <FormItem label="排序" prop="Sort">
          <Input type="number" v-model="tempData.Sort" />
        </FormItem>
      </Form>
      <div slot="footer">
        <Button type="text" @click="modalData = false">取消</Button>
        <Button type="primary" :loading="loading" @click="SaveAuth"
          >确定</Button
        >
      </div>
    </Modal>
  </div>
</template>

<script>
import { GetAuth, DeleteAuth, SaveAuth } from '@/api/rabc'
export default {
  name: 'auth',
  data () {
    return {
      ruleValidate: {
        UniqueCode: [
          {
            required: true,
            message: '不能为空',
            trigger: 'blur'
          }
        ],
        UniqueName: [
          {
            required: true,
            message: '不能为空',
            trigger: 'blur'
          }
        ]
      },
      loading: false,
      loadingTable: false,
      modalData: false,
      tempData: {
        UniqueName: ''
      },
      tableTitle: [
        {
          type: 'index',
          title: '序号',
          align: 'center',
          width: 100
        },
        {
          title: '操作',
          align: 'center',
          width: 200,
          render: (h, params) => {
            return h('div', [
              h(
                'Button',
                {
                  props: {
                    type: 'primary',
                    size: 'small'
                  },
                  style: {
                    marginRight: '5px'
                  },
                  on: {
                    click: () => {
                      var vm = this
                      vm.tempData = params.row
                      vm.modalData = true
                    }
                  }
                },
                '修改'
              ),
              h(
                'Button',
                {
                  props: {
                    type: 'error',
                    size: 'small'
                  },
                  on: {
                    click: () => {
                      this.DeleteAuth(params.row.Id)
                    }
                  }
                },
                '删除'
              )
            ])
          }
        },
        {
          key: 'UniqueName',
          title: '权限名称',
          width: 250,
          align: 'center'
        },
        {
          key: 'UniqueCode',
          title: '唯一编码',
          width: 250,
          align: 'center'
        },
        {
          key: 'Sort',
          title: '排序',
          width: 150,
          align: 'center'
        },
        {
          key: '',
          title: ''
        }
      ],
      tableData: []
    }
  },
  computed: {},
  methods: {
    SaveAuth () {
      debugger
      let vm = this
      vm.$refs['formValidate'].validate((valid) => {
        if (valid) {
          vm.loading = true
          SaveAuth(vm.tempData).then((res) => {
            vm.loading = false

            const data = res.data
            if (data.StatusCode == 200) {
              vm.modalData = false
              vm.$Message.success({ title: '提示', content: '保存成功' })
              vm.GetAuth()
            } else {
              vm.$Message.error({ title: '提示', content: data.Errors })
            }
          })
        }
      })
    },
    GetAuth () {
      GetAuth().then((res) => {
        var data = res.data
        if (data.StatusCode == 200) {
          this.tableData = data.Data
        } else {
          this.$Notice.error({
            title: data.Errors
          })
        }
      })
    },
    DeleteAuth (id) {
      this.$Modal.confirm({
        title: '提示',
        content: '确定要删除吗？',
        onOk: () => {
          DeleteAuth(id).then((res) => {
            const data = res.data
            if (data.StatusCode == 200) {
              this.GetAuth()
              this.$Message.success({ title: '提示', content: '删除成功' })
            } else {
              this.$Message.error({ title: '提示', content: data.Errors })
            }
          })
        },
        onCancel: () => {}
      })
    },
    AddData () {
      var vm = this
      vm.modalData = true
      vm.tempData = {
        UniqueName: ''
      }
    }
  },

  mounted () {
    this.GetAuth()
  }
}
</script>

<style>
</style>
