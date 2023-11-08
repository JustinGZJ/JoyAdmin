<template>
 <div>
   <Card style="margin-bottom: 10px">
<!--     <Date-picker type="daterange" :value="date_range"  placeholder="选择日期" style="width: 250px"></Date-picker>-->
     <Date-picker type="datetimerange" v-model="date_range" format="yyyy-MM-dd HH:mm" placeholder="选择日期和时间" style="width: 300px"></Date-picker>
     <Button type="primary" @click="query" icon="md-add" style="margin-left:10px ">查询</Button>
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
     :page-size-opts="[10,20, 50, 100, 300, 500, 1000, 2000, 5000]"
     @on-change="
        (s) => {
          page = s;
          query();
        }
      "
     @on-page-size-change="
        (p) => {
          size = p;
          page = 1;
          query();
        }
      "
     show-total
     show-sizer
   />
 </div>
</template>

<script>
import { GetData } from '@/api/machine_data'
import dayjs from 'dayjs'
export default {
  name: 'data',
  data () {
    return {
      date_range: ['2016-01-01', '2016-02-15'],
      total: 0,
      page: 1,
      size: 50,
      tableHeight: 450,
      tableTitle: [
        {
          key: 'Order',
          title: '序号',
          minWidth: 100,
          align: 'center'
        },
        {
          key: 'Name',
          title: '名称',
          minWidth: 100,
          align: 'center'
        },
        {
          key: 'Code',
          title: '代码',
          minWidth: 100,
          align: 'center'
        },
        {
          key: 'Result',
          title: '结果',
          minWidth: 100,
          align: 'center'
        },
        {
          key: 'Description',
          title: '描述',
          minWidth: 100,
          align: 'center'
        },
        {
          key: 'Content',
          title: '内容',
          minWidth: 100,
          align: 'center'
        },
        {
          key: 'Time',
          title: '时间',
          minWidth: 100,
          align: 'center'
        }
      ],
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
        End: vm.date_range[1].toLocaleString(),
        page: vm.page,
        size: vm.size
      }
      GetData(reqData).then((res) => {
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
    }
  },
  mounted () {
    this.date_range = [dayjs().startOf('day').format(), dayjs().endOf('day').format()]
    this.tableHeight = window.innerHeight - 260
  }
}
</script>

<style scoped>

</style>
