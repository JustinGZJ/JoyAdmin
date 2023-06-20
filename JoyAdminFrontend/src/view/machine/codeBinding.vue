<template>
  <div>
    <Table :loading="loading" :columns="columns" :data="tableData" :height="tableHeight"></Table>
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
import { GetBindingData } from '@/api/machine_data'

export default {
  data () {
    return {
      columns: [
        {
          title: '编号',
          key: 'Id'
        },
        {
          title: '壳体码',
          key: 'ShellCode'
        },
        {
          title: '定子码',
          key: 'StatorCode'
        },
        {
          title: '转子码',
          key: 'RotorCode'
        },
        {
          title: '插入时间',
          key: 'CreateTime'
        }
      ],
      Data: [],
      total: 0,
      page: 1,
      size: 20,
      loading: false,
      tableHeight: 450
    }
  },
  methods: {
    query () {
      this.loading = true
      GetBindingData(this.page, this.size).then((res) => {
        this.loading = false
        const data = res.data
        if (data.StatusCode === 200) {
          this.Data = data.Data.Items
          this.total = data.Data.TotalCount
        } else {
          this.$Notice.error({
            title: data.Errors
          })
        }
      })
    }

  },
  mounted () {
    this.tableHeight = window.innerHeight - 160
    this.query()
  },
  computed: {
    tableData () {
      return this.Data.map((item, index) => {
        return {
          ...item
        }
      })
    }
  }
}

</script>

<style scoped lang="less">

</style>
