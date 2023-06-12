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
import production from '@/api/production'

export default {
  data() {
    return {
      columns: [
        {
          title: '时间',
          key: 'Time'
        },
        {
          title: '设备',
          key: 'Device'
        },
        {
          title: '类型',
          key: 'ProductionType'
        },
        {
          title: '原因',
          key: 'Reason'
        }, {
          title: '数量',
          key: 'Quantity'
        }
      ],
      Data: [],
      total: 0,
      page: 1,
      size: 20,
      loading: false,
      tableHeight:450
    }
  },
  methods: {
    query() {

      this.loading = true
      production.GetRecentUpload(this.page, this.size).then((res) => {
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
  mounted() {
    this.tableHeight=window.innerHeight-160
    this.query()

  },
  computed: {
    tableData() {
      if (Array.isArray(this.Data) && this.Data.length > 0) {
        return this.Data.map((item) => {
          return {
            Time: item.Time,
            Device: item.Device,
            ProductionType: item.ProductionType===0?'上料':item.ProductionType===1?"OK":"NG",
            Reason: item.Reason,
            Quantity:item.Quantity
          }
        })
      } else {
        return []
      }
    }
  }
}

</script>

<style scoped lang="less">

</style>
