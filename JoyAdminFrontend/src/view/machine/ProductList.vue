<template>
  <div>
    <div style="display:flex;justify-content: space-between">
      <div style="display: flex;align-items: center">
        <Icon size="20" type="md-apps"/>
        <h3>产品信息</h3>
      </div>
      <div>
        <Button type="primary" @click="addProduct">新增</Button>
      </div>
    </div>
    <Table :columns="columns" :data="products"></Table>
    <div class="pagination">
      <Page
        :current="currentPage"
        :page-size="pageSize"
        :total="total"
        @on-change="handlePageChange"
        show-total
      ></Page>
    </div>
    <Modal v-model="modalVisible" title="编辑产品信息">
      <Form :model="form" :rules="rules">
        <Row type="flex" justify="space-between" :gutter="20">
          <i-col span="8">
            <FormItem label="产品编码" prop="ProductCode">
              <Input v-model="form.ProductCode"></Input>
            </FormItem>
          </i-col>
          <i-col span="8">
            <FormItem label="产品名称" prop="ProductName">
              <Input v-model="form.ProductName"></Input>
            </FormItem>
          </i-col>
          <i-col span="8">
            <FormItem label="单位" prop="Unit_Id">
              <Input v-model="form.Unit_Id"></Input>
            </FormItem>
          </i-col>
        </Row>

        <Row type="flex" justify="space-between" :gutter="20">
          <i-col span="8">
            <FormItem label="产品规格" prop="ProductStandard">
              <Input v-model="form.ProductStandard"></Input>
            </FormItem>
          </i-col>
          <i-col span="8">
            <FormItem label="工序" prop="Process_Id">
              <Input v-model="form.Process_Id"></Input>
            </FormItem>
          </i-col>
          <i-col span="8">
            <FormItem label="是否成品" prop="FinishedProduct">
<!--              <ISwitch v-model="form.FinishedProduct"></ISwitch>-->
              <i-switch v-model="form.FinishedProduct"></i-switch>
            </FormItem>
          </i-col>
        </Row>

        <Row type="flex" justify="space-between" :gutter="20">
          <i-col span="8">
            <FormItem label="最大库存量" prop="MaxInventory">
              <Input v-model="form.MaxInventory"></Input>
            </FormItem>
          </i-col>
          <i-col span="8">
            <FormItem label="最小库存量" prop="MinInventory">
              <Input v-model="form.MinInventory"></Input>
            </FormItem>
          </i-col>
          <i-col span="8">
            <FormItem label="安全库存量" prop="SafeInventory">
              <Input v-model="form.SafeInventory"></Input>
            </FormItem>
          </i-col>
        </Row>

        <Row type="flex" justify="space-between" :gutter="20">
          <i-col span="8">
            <FormItem label="当前库存量" prop="InventoryQty">
              <Input v-model="form.InventoryQty"></Input>
            </FormItem>
          </i-col>
          <i-col span="8">
            <FormItem label="创建时间" prop="CreateDate">
              <Input v-model="form.CreateDate"></Input>
            </FormItem>
          </i-col>
          <i-col span="8">
            <FormItem label="创建人" prop="Creator">
              <Input v-model="form.Creator"></Input>
            </FormItem>
          </i-col>
        </Row>

        <Row type="flex" justify="space-between" :gutter="20">
          <i-col span="8">
            <FormItem label="修改时间" prop="ModifyDate">
              <Input v-model="form.ModifyDate"></Input>
            </FormItem>
          </i-col>
          <i-col span="8">
            <FormItem label="修改人" prop="Modifier">
              <Input v-model="form.Modifier"></Input>
            </FormItem>
          </i-col>
          <!-- Add an empty i-col to create space on the right -->
          <i-col span="8"></i-col>
        </Row>

        <!-- Buttons for submitting or canceling the form -->
        <div slot="footer">
          <Button @click.native.prevent="modalVisible = false">取消</Button>
          <Button type="primary" @click.native.prevent="submitForm">确定</Button>
        </div>

      </Form>
    </Modal>

  </div>
</template>

<script>

import {getProductList} from "@/api/Product";

export default {
  data () {
    return {
      products: [],
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
          key: 'Unit_Id'
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
          title: '工序',
          key: 'Process_Id'
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
                      this.editProduct(params.row)
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
                      this.deleteProduct(params.row)
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
      modalVisible: false,
      form: {
        Product_Id: 0,
        ProductCode: '',
        ProductName: '',
        Unit_Id: 0,
        ProductStandard: '',
        ProductAttribute: '',
        Process_Id: 0,
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
        ProductStandard: [{ required: true, message: '请输入产品规格', trigger: 'blur' }],
        ProductAttribute: [{ required: true, message: '请输入产品属性', trigger: 'blur' }],
        MaxInventory: [{ required: true, message: '请输入最大库存量', trigger: 'blur' }],
        MinInventory: [{ required: true, message: '请输入最小库存量', trigger: 'blur' }],
        SafeInventory: [{ required: true, message: '请输入安全库存量', trigger: 'blur' }],
        InventoryQty: [{ required: true, message: '请输入当前库存量', trigger: 'blur' }],
        FinishedProduct: [{ required: true, message: '请输入是否成品', trigger: 'blur' }],
        CreateDate: [{ required: true, message: '请输入创建时间', trigger: 'blur' }],
        Creator: [{ required: true, message: '请输入创建人', trigger: 'blur' }],
        ModifyDate: [{ required: true, message: '请输入修改时间', trigger: 'blur' }],
        Modifier: [{ required: true, message: '请输入修改人', trigger: 'blur' }]
      }
    }
  },
  mounted () {
    this.getProducts()
  },
  methods: {
    getProducts () {
      // 使用 $http 方法从后端获取数据
        getProductList().then(res => {
          this.products = res.data
          this.total = res.data.length
        })
    },
    addProduct () {
      this.modalVisible = true
    },
    handlePageChange (page) {
      this.currentPage = page
      this.getProducts()
    },
    editProduct (product) {
      // 打开编辑模态框
      this.modalVisible = true
      // 将表单数据赋值为当前编辑的产品信息
      this.form = { ...product }
    },
    deleteProduct (product) {
      // 使用 $http 方法删除产品信息
      const url = `/api/products/${product.Product_Id}`
      this.$http.delete(url).then(() => {
        // 删除成功后重新获取数据
        this.getProducts()
      })
    },
    submitForm () {
      // 提交表单数据
      const url = `/api/products/${this.form.Product_Id}`
      const method = this.form.Product_Id ? 'put' : 'post'
      this.$refs.form.validate(valid => {
        if (valid) {
          this.$http[method](url, this.form).then(() => {
            // 提交成功后关闭模态框并重新获取数据
            this.modalVisible = false
            this.getProducts()
          })
        }
      })
    },
    cancelModal () {
      // 取消编辑或添加操作，关闭模态框
      this.modalVisible = false
      this.$refs.form.resetFields()
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
