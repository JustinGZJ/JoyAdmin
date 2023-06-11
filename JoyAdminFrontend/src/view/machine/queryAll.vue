<template>
  <div>
    <Card style="margin-bottom: 10px">
      <!--     <Date-picker type="daterange" :value="date_range"  placeholder="选择日期" style="width: 250px"></Date-picker>-->
      <Date-picker type="datetimerange" v-model="date_range" format="yyyy-MM-dd HH:mm" placeholder="选择日期和时间"
                   style="width: 300px"></Date-picker>
      <Button type="primary" @click="query" icon="md-add" style="margin-left:10px ">查询</Button>
      <Button type="primary" @click="exportCsv" icon="ios-download-outline" :loading="exportLoading" style="margin-left:10px ">导出</Button>
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
    <div>共{{total}}条记录</div>
  </div>
</template>

<script>
import { GetProductData } from '@/api/machine_data'
import excel from '@/libs/excel'

export default {
  name: 'data',
  data () {
    return {
      date_range: ['2016-01-01', '2016-02-15'],
      tableHeight: 450,
      exportLoading: false,
      tableData: [],
      loadingTable: false
    }
  },
  methods: {
    query () {
      let vm = this
      vm.loadingTable = true
      const reqData = {
        Start: vm.date_range[0].toLocaleString(),
        End: vm.date_range[1].toLocaleString()
      }
      GetProductData(reqData).then((res) => {
        vm.loadingTable = false
        const data = res.data
        if (data.StatusCode === 200) {
          vm.tableData = data.Data
        } else {
          vm.$Notice.error({
            title: data.Errors
          })
        }
      })
    },
    exportCsv () {
      if (this.tableData.length) {
        this.exportLoading = true
        const params = {
          title: this.tableTitle.map((item) => {
            return item.title
          }),
          key: this.tableTitle.map((item) => {
            return item.key
          }),
          data: this.tableData,
          autoWidth: true,
          filename: '产品数据' + this.date_range[0].valueOf()
        }
        excel.export_array_to_excel(params)
        this.exportLoading = false
      } else {
        this.$Notice.error('表格数据不能为空！')
      }
    }
  },
  computed: {
    tableTitle () {
      if (Array.isArray(this.tableData) && this.tableData.length > 0) {
        const keys = Object.keys(this.tableData[0])
        return keys.map((key) => {
          return {
            key: key,
            title: key,
            minWidth: 150,
            align: 'center'
          }
        })
      } else {
        return []
      }
      // return this.data
    },
    total () {
      return this.tableData.length
    }
  },
  mounted () {
    let now = new Date()
    let today = now.getFullYear() + '-' + (now.getMonth() + 1) + '-' + now.getDate()
    let tomorrow = now.getFullYear() + '-' + (now.getMonth() + 1) + '-' + (now.getDate() + 1)
    this.date_range = [today, tomorrow]
    this.tableHeight = window.innerHeight - 260
  }
}
</script>

<style scoped>

</style>
