<template>
  <div>
    <div>
      <Select v-model="station" style="width:200px">
        <Option v-for="item in stations" :value="item.value" :key="item.value">{{ item.label }}</Option>
      </Select>
      <Date-picker type="datetimerange" v-model="date_range" format="yyyy-MM-dd HH:mm" placeholder="选择日期和时间"
                   style="width: 300px"></Date-picker>
      <Select v-model="aggregation" style="width:200px">
        <Option v-for="item in aggregationSet" :value="item.value" :key="item.value">{{ item.label }}</Option>
      </Select>
      <Button @click.prevent="query">查询</Button>
    </div>
  </div>
</template>
<script>
import Production from '@/api/production'
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
      aggregation: 0,
      aggregationSet: [
        {
          value: 0,
          label: 'NONE'
        }, {
          value: 1,
          label: 'HOUR'
        }, {
          value: 2,
          label: 'DAY'
        }, {
          value: 3,
          label: 'MONTH'
        }],
      date_range: ['2016-01-01', '2016-02-15'],
      Data: [],
      tableHeight: 450
    }
  },
  methods: {
    query () {
      let station = this.station
      if (this.station === '所有') {
        station = ''
      }
      this.loading = false
      Production.GetPassRates({
        Device: station,
        Start: this.date_range[0].toLocaleString(),
        End: this.date_range[1].toLocaleString(),
        Aggregation: this.aggregation
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
    this.tableHeight = window.innerHeight - 200
    this.query()
  },
  watch: {
    date_range (val) {
      console.log(val)
      let diff = dayjs(val[1]).diff(dayjs(val[0]), 'day')
      if (diff <= 1) {
        this.aggregation = 1
      } else if (diff < 31) {
        this.aggregation = 2
      } else {
        this.aggregation = 3
      }
    }
  }
}

</script>

<style scoped lang="less">

</style>
