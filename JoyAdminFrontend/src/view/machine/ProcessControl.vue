<template>
  <Div>
    <Card style="margin-bottom: 10px">
      <Input v-model="code" placeholder="二维码" style="width: 200px"/>
      <Button type="primary" @click="query" icon="md-add" style="margin-left:10px ">查询</Button>
    </Card>
    <card>
      <Row>
        <Col span="8">
          <p>条码: {{ CodeStatus.BarCode }}</p>
          <p>产品名称: {{ CodeStatus.ProductName }}</p>
          <p>当前工序: {{ CodeStatus.CurrentProcessName }}</p>
          <p>状态: {{ CodeStatus.Status }}</p>
          <Steps :current="step" direction="vertical">
            <Step :title="value" :key="index" v-for="(value,index) in CodeStatus.ProcessNames"></Step>
          </Steps>
        </Col>
        <Col span="16">
          <Table :columns="Columns" :data="CodeStatus.ProcessRecords"></Table>
        </Col>
      </Row>
      <div>
        <Row>
          产品:
          <Select v-model="Product" style="width: 200px">
            <Option v-for="item in Products" :value="item" :key="item">{{item}}</Option>
          </Select>
          工序:
          <Input v-model="Process" style="width: 200px"></Input>
          <Button type="primary" icon="md-add" style="margin-left:10px " @click="">条码验证</Button>
        </Row>
      </div>

    </card>
  </Div>
</template>
<script>
import {GetProcessRecordByBarCode} from "@/api/ProcessControl";

export default {
  data() {
    return {
      code: '',
      Columns: [
        {
          title: '进入时间',
          key: 'EnterTime',
          align: 'center',
          width: 200
        },
        {
          title: '离开时间',
          key: 'LeaveTime',
          align: 'center',
          width: 200
        },
        {
          title: '工序名称',
          key: 'ProcessName',
          align: 'center',
          width: 200
        },
        {
          title: '结果',
          key: 'Result',
          align: 'center',
          width: 200
        }
      ],
      CodeStatus: {
        "BarCode": "",
        "ProductName": "",
        "CurrentProcessName": "",
        "Status": "OK",
        "ProcessNames": [
          ""
        ],
        "ProcessRecords": [
          {
            "EnterTime": "",
            "LeaveTime": "",
            "ProcessName": "",
            "DataId": 0,
            "Result": true
          }
        ]
      },
      Products:["MTB-S","CITY-R","MTB"],
      Process:"定子绕线1",
      Product:"MTB-S",
      Msg:""
    }
  },
  methods: {
    query() {
      GetProcessRecordByBarCode(this.code).then(res => {
        if (res.data.StatusCode === 200) {
          this.CodeStatus = res.data.Data
        } else {
          this.$Message.error(res.data.Message)
        }
      })
    }
  },
  mounted() {
    // this.query()
  },
  computed: {
    step() {
      let index = undefined
      if (this.CodeStatus === null || this.CodeStatus.ProcessNames === null) {
        return index
      }
      //console.log(this.CodeStatus.ProcessNames)
      for (let i = 0; i < this.CodeStatus.ProcessNames.length; i++) {
        if (this.CodeStatus.ProcessNames[i] === this.CodeStatus.CurrentProcessName) {
          index = i
        }
      }
      return index
    }
  },
}
</script>
<style scoped lang="less">

</style>
