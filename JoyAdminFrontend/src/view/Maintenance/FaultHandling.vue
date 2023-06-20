<template>
  <div>
    <Tabs @on-click="tabClick">
      <TabPane label="未处理" name="todo">
        <Table
          :data="todoList"
          :columns="todoColumns"
          border
          style="width: 100%">
        </Table>
      </TabPane>
      <TabPane label="已处理" name="done">
        <Table :loading="loadingDoneList" :columns="doneListColumns" :data="doneList" :height="tableHeight"></Table>
        <Page
          :total="Total"
          :current="Page"
          :page-size="Size"
          :page-size-opts="[10,20, 50, 100, 300, 500, 1000, 2000, 5000]"
          @on-change="
        (s) => {
          Page = s;
          queryHandled();
        }
      "
          @on-page-size-change="
        (p) => {
          Size = p;
          Page = 1;
          queryHandled();
        }
      "
          show-total
          show-sizer
        />
      </TabPane>

    </Tabs>
    <Modal
      title="处理请求"
      v-model="modalVisible"
      :loading="modalLoading"
      :mask-closable="false"
      :closable="true"
      @on-ok="handleOk"
      @on-cancel="handleCancel">
      <Form :model="currentItem" :label-width="80">
        <FormItem label="操作人">
          <Input v-model="currentItem.Operator" placeholder="请输入操作人" />
        </FormItem>
        <FormItem label="操作信息">
          <Input v-model="currentItem.completionMessage" placeholder="请输入操作信息" />
        </FormItem>
      </Form>
    </Modal>
  </div>
</template>

<script>
import DeviceRequest from '@/api/deviceRequest'
import dayjs from 'dayjs'
export default {
  data () {
    return {
      timer: null,
      doneList: [
      ],
      todoList: [
      ],
      loadingDoneList: false,
      loadingTodoList: false,
      modalLoading: false,
      modalVisible: false,
      currentItem: {},
      Page: 1,
      Size: 50,
      Total: 0,
      tableHeight: 450,
      doneListColumns: [
        {
          title: '设备名称',
          key: 'DeviceName'
        },
        {
          title: '故障时间',
          key: 'RequestTime'
        },
        {
          title: '故障描述',
          key: 'RequestMessage'
        },
        {
          title: '是否处理',
          key: 'IsHandled'
        },
        {
          title: '处理时间',
          key: 'CompletionTime'
        },
        {
          title: '处理人',
          key: 'Operator'
        },
        {
          title: '处理内容',
          key: 'CompletionMessage'
        }
      ],
      todoColumns: [
        {
          title: '设备名称',
          key: 'DeviceName'
        },
        {
          title: '故障时间',
          key: 'RequestTime'
        },
        {
          title: '故障描述',
          key: 'RequestMessage'
        },
        {
          title: '操作',
          key: 'handle',
          render: (h, { row }) => {
            return h('Button', {
              props: {
                type: 'primary',
                size: 'small'
              },
              on: {
                click: () => {
                  this.showModal(row)
                }
              }
            }, '处理')
          }
        }
      ]
    }
  },
  methods: {
    tabClick (name) {
      if (name === 'done') {
        this.queryHandled()
      }
    },
    queryHandled () {
      let { Page, Size } = this
      // GetDeviceRequests?Start=2021-01-01&End=2024-01-01&DeviceName=%E5%AE%9A%E5%AD%901&Page=1&Size=50'
      DeviceRequest.GetDeviceRequests({
        Start: dayjs().add(-30, 'day').format('YYYY-MM-DD HH:mm:ss'),
        End: dayjs().format('YYYY-MM-DD HH:mm:ss'),
        Handled: true,
        Page: Page,
        Size: Size
      }).then(res => {
        if (res.data.StatusCode === 200) {
          this.Total = res.data.Data.TotalCount
          this.doneList = res.data.Data.Items
        } else {
          this.$Message.error(res.data.Message)
        }
      })
    },
    queryTodo () {
      this.loadingTodoList = true
      DeviceRequest.GetUnHandled().then(res => {
        if (res.data.StatusCode === 200) {
          this.todoList = res.data.Data
        } else {
          this.$Notice.error(res.data.Message)
        }
      }).finally(() => {
        this.loadingTodoList = false
      })
    },
    handleOk () {
      this.modalLoading = true
      console.log(this.currentItem)
      // Update the item in the database using an API call
      DeviceRequest.UpdateDeviceRequest(
        { Id: this.currentItem.Id,
          operator: this.currentItem.Operator,
          completionMessage: this.currentItem.completionMessage
        }).then(() => {
        this.queryTodo()
        this.modalVisible = false
        this.modalLoading = false
      })
    },
    handleCancel () {
      console.log('Clicked cancel')
      this.modalVisible = false
    },
    showModal (item) {
      this.currentItem = item
      this.modalVisible = true
    }
  },
  mounted () {
    this.queryHandled()
    this.queryTodo()
    this.tableHeight = window.innerHeight - 220
    this.timer = setInterval(() => {
      this.queryTodo()
    }, 10000)
  },
  beforeDestroy () {
    clearInterval(this.timer)
  }
}
</script>
