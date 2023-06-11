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
      :title="tempData.Id > 0 ? '修改角色' : '新增角色'"
      :mask-closable="false"
    >
      <Form
        ref="formValidate"
        :model="tempData"
        :label-width="80"
        :rules="ruleValidate"
      >
        <FormItem label="角色名称" prop="Name">
          <Input v-model="tempData.Name" />
        </FormItem>
        <FormItem label="角色说明">
          <Input v-model="tempData.Remark" />
        </FormItem>
         <FormItem label="排序" prop="Sort">
          <Input type="number" v-model="tempData.Sort" />
        </FormItem>
      </Form>
      <div slot="footer">
        <Button type="text" @click="modalData = false">取消</Button>
        <Button type="primary" :loading="loading" @click="SaveRole"
          >确定</Button
        >
      </div>
    </Modal>

    <Modal
      :width="400"
      v-model="modalAuth"
      title="分配权限"
      :mask-closable="false"
    >
      <div>
        <div style="padding-bottom: 6px; margin-bottom: 6px">
          <Checkbox
            size="large"
            :value="checkAll"
            @click.prevent.native="handleCheckAll"
            >全选</Checkbox
          >
        </div>
        <CheckboxGroup v-model="tempAuthIds" @on-change="checkAllGroupChange">
          <Checkbox
          style="margin-bottom:10px;min-width:100px"
            v-for="item in allAuths"
            :key="item.Id"
            size="large"
            :label="item.Id"
            >{{ item.UniqueName }}</Checkbox
          >
        </CheckboxGroup>
      </div>

      <div slot="footer">
        <Button type="text" @click="modalAuth = false">取消</Button>
        <Button type="primary" :loading="loading" @click="GiveRoleSecurity"
          >确定</Button
        >
      </div>
    </Modal>
  </div>
</template>

<script>
import {
  GetRole,
  DeleteRole,
  SaveRole,
  GetAuthByRoleId,
  GetAuth,
  GiveRoleSecurity
} from '@/api/rabc'
export default {
  name: 'role',
  data () {
    return {
      ruleValidate: {
        Name: [
          {
            required: true,
            message: '不能为空',
            trigger: 'blur'
          }
        ]
      },
      loading: false,
      loadingTable: false,
      tempData: {
        Name: '',
        Remark: ''
      },
      modalData: false,
      modalAuth: false,
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
          width: 220,
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
                  style: {
                    marginRight: '5px'
                  },
                  on: {
                    click: () => {
                      this.DeleteRole(params.row.Id)
                    }
                  }
                },
                '删除'
              ),
              h(
                'Button',
                {
                  props: {
                    type: 'success',
                    size: 'small'
                  },
                  style: {
                    marginRight: '5px'
                  },
                  on: {
                    click: () => {
                      var vm = this
                      vm.tempDataId = params.row.Id
                      vm.GetAuthByRoleId()
                    }
                  }
                },
                '分配权限'
              )
            ])
          }
        },
        {
          key: 'Name',
          title: '角色名称',
          width: 250,
          align: 'center'
        },
        {
          key: 'Remark',
          title: '说明',
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
      tableData: [],
      allAuths: [],
      tempAuthIds: [],
      tempDataId: null,
      checkAll: false
    }
  },
  computed: {},
  methods: {
    SaveRole () {
      let vm = this
      vm.$refs['formValidate'].validate((valid) => {
        if (valid) {
          vm.loading = true
          SaveRole(vm.tempData).then((res) => {
            vm.loading = false
            const data = res.data
            if (data.StatusCode == 200) {
              vm.modalData = false
              vm.$Message.success({ title: '提示', content: '保存成功' })
              vm.GetRole()
            } else {
              vm.$Message.error({ title: '提示', content: data.Errors })
            }
          })
        }
      })
    },
    GetRole () {
      this.loadingTable = true
      GetRole().then((res) => {
        this.loadingTable = false
        var data = res.data
        if (data.StatusCode == 200) {
          this.tableData = data.Data
        } else {
          vm.$Notice.error({
            title: data.Errors
          })
        }
      })
    },
    DeleteRole (id) {
      this.$Modal.confirm({
        title: '提示',
        content: '确定要删除吗？',
        onOk: () => {
          DeleteRole(id).then((res) => {
            const data = res.data
            if (data.StatusCode == 200) {
              this.GetRole()
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
        Name: '',
        Remark: ''
      }
    },
    GetAuthByRoleId () {
      let vm = this
      GetAuthByRoleId(vm.tempDataId).then((res) => {
        var data = res.data
        if (data.StatusCode == 200) {
          vm.tempAuthIds = data.Data.map((item) => {
            return item.Id
          })
          if (
            vm.tempAuthIds.length > 0 &&
            vm.tempAuthIds.length == vm.allAuths.length
          ) {
            this.checkAll = true
          } else {
            this.checkAll = false
          }
          vm.modalAuth = true
        } else {
          vm.$Notice.error({
            title: data.Errors
          })
        }
      })
    },
    GetAuth () {
      GetAuth().then((res) => {
        var data = res.data
        if (data.StatusCode == 200) {
          this.allAuths = data.Data
        } else {
          vm.$Notice.error({
            title: data.Errors
          })
        }
      })
    },
    GiveRoleSecurity () {
      let vm = this
      var reqData = {
        RoleId: this.tempDataId,
        SecurityIds: this.tempAuthIds
      }
      vm.loading = true
      GiveRoleSecurity(reqData).then((res) => {
        vm.loading = false

        var data = res.data
        if (data.StatusCode == 200) {
          vm.modalAuth = false
          vm.$Message.success({ title: '提示', content: '操作成功' })
          vm.GetRole()
          vm.GetAuth()
        } else {
          vm.$Notice.error({
            title: data.Errors
          })
        }
      })
    },
    handleCheckAll () {
      let vm = this
      vm.checkAll = !vm.checkAll
      if (vm.checkAll) {
        vm.tempAuthIds = vm.allAuths.map((item) => {
          return item.Id
        })
      } else {
        vm.tempAuthIds = []
      }
    },
    checkAllGroupChange (data) {
      let vm = this
      if (
        vm.tempAuthIds.length > 0 &&
        vm.tempAuthIds.length == vm.allAuths.length
      ) {
        this.checkAll = true
      } else {
        this.checkAll = false
      }
    }
  },
  mounted () {
    this.GetRole()
    this.GetAuth()
  }
}
</script>

<style>
</style>
