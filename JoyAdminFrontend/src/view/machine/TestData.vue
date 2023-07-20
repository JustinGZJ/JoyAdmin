<template>

    <div >
      <div  style="margin-left: 200px">
        起始时间:
        <DatePicker  class="ivu-date-picker" type="datetime" v-model="headerForm.dtFrom"  format="yyyy-MM-dd HH:mm"
                    placeholder="请选择起始时间" @on-change="dateChange" style="width: 200px"></DatePicker>
        结束时间
        <DatePicker  type="datetime" v-model="headerForm.dtTo"  format="yyyy-MM-dd HH:mm"
                    placeholder="请选择结束时间" @on-change="dateChange" style="width: 200px"></DatePicker>
        <Button style="margin: 0 10px;" type="primary" @click="handleQueryClicked">查询</Button>
        <Button style="margin: 0 10px;" icon="ios-download-outline" @click="exportExcel">导出文件</Button>
      </div>
      <layout>
      <sider span="auto" :style="{position: 'relative', height: '100%', left: 0}">
        <Menu accordion  active-name:="siderParams.activeName"  @on-select="handleStationChanged" title="站位数据" style="height: 100px"  width="auto" >
          <Submenu v-for="(value,key) in siderParams.stations"  :key="key" :name="key">
            <template slot="title">
              <Icon type="ios-stats" />
              {{ key }}
            </template>
            <MenuItem v-for="item in value" :key="item" :name="item">{{item}}</MenuItem>
          </Submenu>
        </Menu>
      </sider>
      <content>
        <!-- <Card> -->
        <div>
          <tables searchable ref="tables"
                  :loading="tableConfig.loading"
                  :height="tableConfig.height"
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
    </div >
</template>

<script>
import Tables from '_c/tables'
import machine_data from '@/api/machine_data'
import { getStations } from '@/api/get_status'
import dayjs from 'dayjs'

export default {
  components: {
    Tables
  },
  data () {
    return {
      page: 1,
      pageSize: 100,
      total: 0,
      tableConfig: {
        loading: false,
        columns: [],
        tableData: [],
        height: 600
      },
      siderParams: {
        stations: {},
        activeName: ''
      },
      headerForm: {
        dtFrom: dayjs().add(-1, 'day').startOf('day').toDate(),
        dtTo: dayjs().endOf('day').toDate()
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
    dateChange (date) {

    },
    queryData () {
      this.tableConfig.loading = true
      this.tableConfig.tableData = []
      machine_data.GetProductDataByName({
        start: dayjs(this.headerForm.dtFrom).format('YYYY-MM-DD HH:mm:ss'),
        end: dayjs(this.headerForm.dtTo).format('YYYY-MM-DD HH:mm:ss'),
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
                sortable: true
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
      let { id } = this.current_station_info
      exportExcel({
        stationId: id,
        From: dayjs(this.headerForm.dtFrom).add(8, 'hour').toDate(),
        To: dayjs(this.headerForm.dtTo).add(8, 'hour').toDate()
      })
        .then(res => {
          console.log(res)
          // 地址转换
          let url = window.URL.createObjectURL(resata)
          // 文件名
          let fileName = `${this.headerForm.dtFrom}-${this.headerForm.dtTo}.csv`
          const a = document.createElement('a')
          a.setAttribute('href', url)
          a.setAttribute('download', fileName)
          document.body.append(a)
          a.click()
          document.body.removeChild(a)
        }).catch(err => {
          this.$Message.error(err)
        })
    },
    getHeight () {
      this.tableConfig.height = window.innerHeight - 260
    },
    getStations () {
      getStations()
        .then(response => {
          // console.log(response)
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
    this.getHeight()
    this.getStations()
    window.addEventListener('resize', this.getHeight)
  },
  destroyed () {
    window.removeEventListener('resize', this.getHeight)
  }
}
</script>

<style>
  .ivu-menu-item-selected {
    background-color: #e6f7ff;
  }
  .ivu-date-picker  {
    z-index: 2000;
  }
</style>
