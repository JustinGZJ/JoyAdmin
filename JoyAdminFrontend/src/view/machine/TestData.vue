<template>

  <div id="testData">

    <layout>
      <sider span="auto" :style="{position: 'relative', height: '100%', left: 0}">
        <Menu active-name:="siderParams.activeName" @on-select="handleStationChanged" title="站位数据" width="auto">
          <Submenu v-for="(value,key) in siderParams.stations" :key="key" :name="key">
            <template slot="title">
              <Icon type="ios-stats"/>
              {{ key }}
            </template>
            <MenuItem v-for="item in value" :key="item" :name="item">{{ item }}</MenuItem>
          </Submenu>
        </Menu>
      </sider>
      <content id="testDataContent">
        <!-- <Card> -->
        <div id="testDataOption" style="display: flex; flex-wrap: wrap; gap: 10px;">

          <div>
            <label>起始时间:</label>
            <DatePicker class="ivu-date-picker" type="datetime" v-model="headerForm.dtFrom" format="yyyy-MM-dd HH:mm"
                        placeholder="请选择起始时间" @on-change="dateChange" style="width: 200px"></DatePicker>
          </div>

          <div>
            <label>结束时间:</label>
            <DatePicker type="datetime" v-model="headerForm.dtTo" format="yyyy-MM-dd HH:mm"
                        placeholder="请选择结束时间" @on-change="dateChange" style="width: 200px"></DatePicker>
          </div>

          <div>
            <label>工单号：</label>
            <Input v-model="headerForm.WorkOrder" style="width: 150px" placeholder="工单号"></Input>
          </div>

          <div>
            <label>机种：</label>
            <Input v-model="headerForm.Model" style="width: 150px" placeholder="机种"></Input>
          </div>

          <div>
            <label>选择字段：</label>
            <Select v-model="cpkColumn" style="width: 200px">
              <Option v-for="item in tableConfig.columns" :value="item.key" :key="item.key">{{ item.title }}</Option>
            </Select>
            <Button type="primary" @click="handleQueryCpk">CPK计算</Button>
          </div>
          <div style="display: flex;gap: 10px">
            <Button type="primary" @click="handleQueryClicked">查询</Button>
            <Button icon="ios-download-outline" @click="exportExcel">导出文件</Button>
          </div>

        </div>

        <div>
          <tables searchable ref="tables"
                  :loading="tableConfig.loading"
                  :height="tableConfig.height"
                  :width="tableConfig.width"
                  v-model="tableConfig.tableData"
                  :columns="tableConfig.columns"/>
        </div>
        <page show-total show-sizer
              :total="total"
              :page-size="pageSize"
              :current="page"
              :page-size-opts="[15, 30, 50, 100,500]"
              @on-page-size-change="handlePageSizeChanged"
              @on-change="handlePageChanged"></page>
      </content>
    </layout>
    <Modal v-model="modal1" title="CPK计算" width="80" @on-ok="handleOk1" @on-cancel="handleCancel1">
      <CpkCalc :input="input" :lsl="lsl" :usl="usl"></CpkCalc>
    </Modal>
  </div>
</template>

<script>
import Tables from '_c/tables'
import machine_data, { GetProductDataByNameNoPage } from '@/api/machine_data'
import dayjs from 'dayjs'
import { export_array_to_excel } from '@/libs/excel'
import { getStations } from '@/api/Process'
import CpkCalc from '@/view/components/CpkCalc.vue'

export default {
  components: {
    CpkCalc,
    Tables
  },
  data () {
    return {
      page: 1,
      pageSize: 100,
      total: 0,
      modal1: false,
      input: '',
      lsl: 0,
      usl: 0,
      cpkColumn: undefined,
      tableConfig: {
        loading: false,
        columns: [],
        tableData: [],
        height: 600,
        width: 600
      },
      siderParams: {
        stations: {},
        activeName: ''
      },
      headerForm: {
        dtFrom: dayjs().add(-1, 'day').startOf('day').toDate(),
        dtTo: dayjs().endOf('day').toDate(),
        WorkOrder: '',
        Model: ''
      }
    }
  },
  methods: {
    handlePageChanged (page) {
      console.log(`page` + page)
      this.page = page
      this.queryData()
    },
    handlePageSizeChanged (pageSize) {
      console.log(`pageSize` + pageSize)
      this.page = 1
      this.pageSize = pageSize
      this.queryData()
    },
    handleStationChanged (e) {
      console.log(e)
      this.tableConfig.tableData = []
      this.siderParams.activeName = e

      this.queryData()
    },
    handleQueryClicked () {
      this.queryData()
    },
    handleQueryCpk () {
      this.modal1 = true
      let data = this.tableConfig.tableData.map(item => {
        // 判定是否是数字，如果是文字的话提取其中的数字
        if (isNaN(item[this.cpkColumn])) {
          let reg = /\d+\.?\d*/g
          let arr = item[this.cpkColumn].match(reg)
          return arr[0]
        }
        return item[this.cpkColumn]
      })
      this.input = data.join(',')
      console.log(this.input)
    },
    handleOk1 () {
      this.modal1 = false
    },
    handleCancel1 () {
      this.modal1 = false
    },

    dateChange (date) {

    },
    queryData () {
      this.tableConfig.loading = true
      this.tableConfig.tableData = []
      machine_data.GetProductDataByName({
        start: dayjs(this.headerForm.dtFrom).format('YYYY-MM-DD HH:mm:ss'),
        end: dayjs(this.headerForm.dtTo).format('YYYY-MM-DD HH:mm:ss'),
        WorkOrder: this.headerForm.WorkOrder,
        Model: this.headerForm.Model,
        name: this.siderParams.activeName,
        page: this.page,
        Size: this.pageSize
      })
        .then(response => {
          let { Data } = response.data
          const {
            TotalCount,
            Items
          } = Data
          this.total = TotalCount
          if (TotalCount > 0) {
            const firtItem = Items[0]
            this.tableConfig.columns = Object.keys(firtItem).map(key => {
              return {
                title: key,
                key: key,
                align: 'center',
                sortable: true,
                minWidth: 150
              }
            })
          }
          this.tableConfig.tableData = Items
        })
        .catch(err => {
          this.$Message.error(err)
        }).finally(() => {
          this.tableConfig.loading = false
        })
    },
    exportExcel () {
      GetProductDataByNameNoPage({
        start: dayjs(this.headerForm.dtFrom).format('YYYY-MM-DD HH:mm:ss'),
        end: dayjs(this.headerForm.dtTo).format('YYYY-MM-DD HH:mm:ss'),
        name: this.siderParams.activeName,
        Model: this.headerForm.Model,
        WorkOrder: this.headerForm.WorkOrder
      })
        .then(res => {
          const { Data, Succeeded, Errors } = res.data
          if (!Succeeded) {
            this.$Notice.error({
              title: '错误',
              desc: Errors
            })
            return
          }
          if (Data.length === 0) {
            this.$Notice.error({
              title: '错误',
              desc: '没有数据'
            })
            return
          }
          console.log(Data)
          let titles = Object.keys(Data[0])
          console.log(titles)
          let param = {
            data: res.data.Data,
            key: titles,
            title: titles,
            filename: this.siderParams.activeName + dayjs().unix() + '.xlsx',
            autoWidth: true
          }
          console.log(param)
          export_array_to_excel(param)
        }).catch(err => {
          this.$Notice.error({
            title: '错误',
            desc: err
          })
        })
    },
    resize () {
      // 设置table的高度window.innerHeight - testdataoption的高度-100
      this.tableConfig.height = window.innerHeight - document.getElementById('testDataOption').offsetHeight - 250
      // 设置table的宽度等于父元素的宽度-200
      this.tableConfig.width = document.getElementById('testData').offsetWidth - 200
    },
    getStations () {
      getStations()
        .then(response => {
          console.log(response)
          let Data = response.data
          this.siderParams.stations = Data.reduce((result, item) => {
            const key = item.substring(0, 2) // 获取前两个字母作为 key
            if (!result[key]) {
              result[key] = [] // 如果 key 不存在，则初始化为空数组
            }
            result[key].push(item) // 将当前元素添加到对应的数组中
            return result
          }, {})
          this.siderParams.activeName = Data[0]
          this.queryData()
        })
    }
  },
  computed: {
    // map时候要使用命名空间
  },
  mounted () {
    this.resize()
    this.getStations()
    window.addEventListener('resize', this.resize)
  },
  destroyed () {
    window.removeEventListener('resize', this.resize)
  }
}
</script>

<style>
.ivu-menu-item-selected {
  background-color: #e6f7ff;
}

</style>
