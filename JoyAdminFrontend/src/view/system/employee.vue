<template>
  <div>
    <Card style="margin-bottom: 10px">
      <Input v-model="keyword" placeholder="账号/姓名" style="width: 200px" />
      <Button
        type="primary"
        style="margin-left: 10px"
        @click="SearchEmployee"
        icon="md-search"
        >查询</Button
      >
      <Button
        type="primary"
        style="margin-left: 10px"
        @click="AddUser"
        icon="md-add"
        >新增</Button
      >
    </Card>
    <Table
      ref="table"
      style="margin-bottom: 10px"
      border
      :loading="loadingTable"
      :columns="tableTitle"
      :data="tableData"
      :height="tableHeight"
    ></Table>
    <Page
      :total="total"
      :current="page"
      :page-size="size"
      :page-size-opts="[10, 50, 100, 300, 500, 1000, 2000, 5000]"
      @on-change="
        (s) => {
          page = s;
          SearchEmployee();
        }
      "
      @on-page-size-change="
        (p) => {
          size = p;
          page = 1;
          SearchEmployee();
        }
      "
      show-total
      show-sizer
    />
    <Modal
      :width="400"
      v-model="modalData"
      :title="tempData.Id > 0 ? '修改员工' : '新增员工'"
      :mask-closable="false"
    >
      <Form
        ref="formValidate"
        :model="tempData"
        :label-width="80"
        :rules="ruleValidate"
      >
        <FormItem label="账号" prop="Account">
          <Input v-model="tempData.Account" />
        </FormItem>
        <FormItem label="姓名" prop="Name">
          <Input v-model="tempData.Name" />
        </FormItem>
        <FormItem label="联系方式" prop="Phone">
          <Input v-model="tempData.Phone" />
        </FormItem>
        <FormItem label="职位" prop="LinkPost">
          <Input v-model="tempData.LinkPost" />
        </FormItem>
        <FormItem label="备注" prop="Remark">
          <Input v-model="tempData.Remark" />
        </FormItem>
      </Form>
      <div slot="footer">
        <Button type="text" @click="modalData = false">取消</Button>
        <Button type="primary" :loading="loading" @click="SaveEmployee"
          >确定</Button
        >
      </div>
    </Modal>

    <Modal
      :width="400"
      v-model="modalAuth"
      title="分配角色"
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
        <CheckboxGroup v-model="tempRoleIds" @on-change="checkAllGroupChange">
          <Checkbox
            style="margin-bottom: 10px; min-width: 100px"
            v-for="item in allRoles"
            :key="item.Id"
            size="large"
            :label="item.Id"
            >{{ item.Name }}</Checkbox
          >
        </CheckboxGroup>
      </div>

      <div slot="footer">
        <Button type="text" @click="modalAuth = false">取消</Button>
        <Button type="primary" :loading="loading" @click="GiveUserRole"
          >确定</Button
        >
      </div>
    </Modal>
  </div>
</template>

<script>
import {
  SearchEmployee,
  DeleteEmployee,
  SaveEmployee,
  GetRoleByUserId,
  GetRole,
  GiveUserRole,
  ResetPwd
} from '@/api/rabc'
export default {
  name: 'employee',
  data () {
    return {
      total: 0,
      page: 1,
      size: 50,
      tableHeight: 450,
      keyword: '',
      ruleValidate: {
        Account: [
          {
            required: true,
            message: '不能为空',
            trigger: 'blur'
          }
        ],
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
          width: 100,
          render: (h, params) => {
            return h(
              'span',
              params.index + (this.page - 1) * this.size + 1
            )
          }
        },
        {
          title: '操作',
          align: 'center',
          width: 240,
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
                      this.DeleteEmployee(params.row.Id)
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
                      vm.GetRoleByUserId()
                    }
                  }
                },
                '分配角色'
              ), h(
                'Button',
                {
                  props: {
                    type: 'warning',
                    size: 'small'
                  },
                  style: {
                    marginRight: '5px'
                  },
                  on: {
                    click: () => {
                      var vm = this
                      vm.ResetPwd(params.row.Id)
                    }
                  }
                },
                '重置密码'
              )
            ])
          }
        },
        {
          key: 'Account',
          title: '账号',
          minWidth: 100,
          align: 'center'
        },
        {
          key: 'Name',
          title: '姓名',
          minWidth: 100,
          align: 'center'
        },
        {
          key: 'Phone',
          title: '联系方式',
          minWidth: 100,
          align: 'center'
        },
        {
          key: 'LinkPost',
          title: '职位',
          minWidth: 100,
          align: 'center'
        },
        {
          key: 'Remark',
          title: '备注',
          minWidth: 100,
          align: 'center'
        }
      ],
      tableData: [],
      allRoles: [],
      tempRoleIds: [],
      tempDataId: null,
      checkAll: false
    }
  },
  computed: {},
  methods: {
    SaveEmployee () {
      let vm = this
      vm.$refs['formValidate'].validate((valid) => {
        if (valid) {
          vm.loading = true
          SaveEmployee(vm.tempData).then((res) => {
            vm.loading = false

            const data = res.data
            if (data.StatusCode == 200) {
              vm.modalData = false
              vm.$Message.success({ title: '提示', content: '保存成功' })
              vm.SearchEmployee()
            } else {
              vm.$Message.error({ title: '提示', content: data.Errors })
            }
          })
        }
      })
    },
    SearchEmployee () {
      let vm = this
      vm.loadingTable = true
      var reqData = {
        page: vm.page,
        size: vm.size,
        keyword: vm.keyword
      }
      SearchEmployee(reqData).then((res) => {
        vm.loadingTable = false
        var data = res.data
        if (data.StatusCode === 200) {
          vm.tableData = data.Data.Items
          vm.total = data.Data.TotalCount
        } else {
          vm.$Notice.error({
            title: data.Errors
          })
        }
      })
    },
    DeleteEmployee (id) {
      this.$Modal.confirm({
        title: '提示',
        content: '确定要删除吗？',
        onOk: () => {
          DeleteEmployee(id).then((res) => {
            const data = res.data
            if (data.StatusCode == 200) {
              this.SearchEmployee()
              this.$Message.success({ title: '提示', content: '删除成功' })
            } else {
              this.$Message.error({ title: '提示', content: data.Errors })
            }
          })
        },
        onCancel: () => {}
      })
    },
    ResetPwd (id) {
      this.$Modal.confirm({
        title: '提示',
        content: '确定要重置密码为“123456”吗？',
        onOk: () => {
          ResetPwd(id).then((res) => {
            const data = res.data
            if (data.StatusCode == 200) {
              this.$Message.success({ title: '提示', content: '重置成功' })
            } else {
              this.$Message.error({ title: '提示', content: data.Errors })
            }
          })
        },
        onCancel: () => { }
      })
    },
    AddUser () {
      var vm = this
      vm.modalData = true
      vm.tempData = {
        Name: '',
        Remark: ''
      }
    },
    GetRoleByUserId () {
      let vm = this
      GetRoleByUserId(vm.tempDataId).then((res) => {
        var data = res.data
        if (data.StatusCode == 200) {
          vm.tempRoleIds = data.Data.map((item) => {
            return item.Id
          })
          if (
            vm.tempRoleIds.length > 0 &&
            vm.tempRoleIds.length == vm.allRoles.length
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
    GetRole () {
      GetRole().then((res) => {
        var data = res.data
        if (data.StatusCode == 200) {
          this.allRoles = data.Data
        } else {
          vm.$Notice.error({
            title: data.Errors
          })
        }
      })
    },
    GiveUserRole () {
      let vm = this
      var reqData = {
        UserId: this.tempDataId,
        RoleIds: this.tempRoleIds
      }
      vm.loading = true
      GiveUserRole(reqData).then((res) => {
        vm.loading = false
        var data = res.data
        if (data.StatusCode == 200) {
          vm.modalAuth = false
          vm.$Message.success({ title: '提示', content: '操作成功' })
          vm.SearchEmployee()
          vm.GetRole()
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
        vm.tempRoleIds = vm.allRoles.map((item) => {
          return item.Id
        })
      } else {
        vm.tempRoleIds = []
      }
    },
    checkAllGroupChange (data) {
      let vm = this
      if (
        vm.tempRoleIds.length > 0 &&
        vm.tempRoleIds.length == vm.allRoles.length
      ) {
        this.checkAll = true
      } else {
        this.checkAll = false
      }
    }
  },
  mounted () {
    this.tableHeight = window.innerHeight - 260
    this.SearchEmployee()
    this.GetRole()
  }
}
</script>

<style>
</style>
