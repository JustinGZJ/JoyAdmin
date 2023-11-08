<template>
  <div>
    <Card style="margin-bottom: 10px">
      <Input v-model="code" placeholder="二维码" style="width: 200px"/>
      <Button type="primary" @click="query" icon="md-add" style="margin-left:10px ">查询</Button>
      <Button type="primary" @click="exportCsv" icon="ios-download-outline" :loading="exportLoading" style="margin-left:10px ">导出</Button>
    </Card>
    <Card>
      <div style="display: flex">
        <Input :value="shellCode" style="width: 300px;margin-right: 5px; ">
          <span slot="prepend" >壳体二维码</span>
        </Input>
        <Input :value="rotorCode" style="width: 300px;margin-right: 5px;">
          <span slot="prepend">转子二维码</span>
        </Input>
        <Input :value="statorCode" style="width: 300px;margin-right: 5px;">
          <span slot="prepend">定子二维码</span>
        </Input>
      </div>

    </Card>
    <Table
      ref="table"
      style="margin-bottom: 10px"
      border
      :loading="loadingTable"
      :columns="tableTitle"
      :data="tableData"
      :max-height="table_max_height"
    ></Table>
  </div>
</template>

<script>

import { GetMachineDataByCode } from '@/api/machine_data'
import excel from '@/libs/excel'

export default {
  name: 'query',
  data () {
    return {
      code: '',
      total: 0,
      page: 1,
      size: 50,
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
      loadingTable: false,
      exportLoading: false,
      table_max_height: 450,
      shellCode: '',
      rotorCode: '',
      statorCode: ''
    }
  },
  methods: {
    query () {
      this.loadingTable = true
      let vm = this
      GetMachineDataByCode(this.code).then((res) => {
        vm.loadingTable = false
        let data = res.data
        if (data.StatusCode === 200) {
          vm.statorCode = data.Data.StatorCode
          vm.rotorCode = data.Data.RotorCode
          vm.shellCode = data.Data.ShellCode
          vm.tableData = data.Data.Items
        } else {
          vm.$Notice.error({
            title: data.Errors
          })
        }
      }).catch(err => {
        vm.loadingTable = false
        vm.$Notice.error({
          title: err
        })
      })
    },
    exportCsv () {
      if (this.tableData.length) {
        this.exportLoading = true
        const params = {
          title: ['序号', '名称', '结果', '描述', '内容', '时间'],
          key: ['Order', 'Name', 'Result', 'Description', 'Content', 'Time'],
          data: this.tableData,
          autoWidth: true,
          filename: this.code,
          preArray: [
            ['壳体二维码', this.shellCode, '', '', '', ''],
            ['定子二维码', this.statorCode, '', '', '', ''],
            ['转子二维码', this.rotorCode, '', '', '', '']
          ]
        }
        excel.export_array_to_excel_custom(params)
        this.exportLoading = false
      } else {
        this.$Notice.error('表格数据不能为空！')
      }
    }

  },
  mounted () {
    this.table_max_height = window.innerHeight - 300
  }
}
</script>

<style scoped>

</style>
