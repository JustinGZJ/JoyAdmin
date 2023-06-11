<template>
  <div>
    <div>
      <Select v-model="station" style="width:200px">
        <Option v-for="item in stations" :value="item.value" :key="item.value">{{ item.label }}</Option>
      </Select>
      <Date-picker type="datetimerange" v-model="date_range" format="yyyy-MM-dd HH:mm" placeholder="选择日期和时间"
                   style="width: 300px"></Date-picker>
      <Button @click.prevent="query">查询</Button>
    </div>
    <Table :loading="loading" :columns="columns" :data="tableData"></Table>
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
import { GetAlarmHistories } from '@/api/alarm'
import dayjs from 'dayjs'

export default {
  data () {
    return {
      station: '所有',
      stations: [
        {
          value: '所有',
          label: '所有'
        },
        {
          value: '定子1',
          label: '定子1'
        },
        {
          value: '定子2',
          label: '定子2'
        },
        {
          value: '定子3',
          label: '定子3'
        },
        {
          value: '定子4',
          label: '定子4'
        },
        {
          value: '定子5',
          label: '定子5'
        },
        {
          value: '定子6',
          label: '定子6'
        },
        {
          value: '定子7',
          label: '定子7'
        },
        {
          value: '转子1',
          label: '转子1'
        },
        {
          value: '转子2',
          label: '转子2'
        },
        {
          value: '转子3',
          label: '转子3'
        },
        {
          value: '转子4',
          label: '转子4'
        },
        {
          value: '合装1',
          label: '合装1'
        },
        {
          value: '合装2',
          label: '合装2'
        },
        {
          value: '合装3',
          label: '合装3'
        }
      ],
      date_range: ['2016-01-01', '2016-02-15'],
      columns: [
        {
          title: '工站',
          key: 'Station'
        },
        {
          title: '信息',
          key: 'Message'
        },
        {
          title: '开始时间',
          key: 'StartTime'
        },
        {
          title: '持续时间',
          key: 'Duration'
        }
      ],
      Data: [],
      total: 0,
      page: 1,
      size: 20,
      loading: false
    }
  },
  methods: {
    query () {
      let station = this.station
      if (this.station === '所有') {
        station = ''
      }
      this.loading = false
      GetAlarmHistories({
        Station: station,
        Start: this.date_range[0].toLocaleString(),
        End: this.date_range[1].toLocaleString(),
        page: this.page,
        size: this.size
      }).then((res) => {
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
    this.date_range = [dayjs().startOf('day').format(), dayjs().endOf('day').format()]
    this.query()
  },
  computed: {
    tableData () {
      if (Array.isArray(this.Data) && this.Data.length > 0) {
        return this.Data.map((item) => {
          return {
            Station: item.Station,
            Message: item.Message,
            StartTime: item.StartTime,
            Duration: dayjs(item.EndTime).diff(item.StartTime, 'second') + '秒'
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
