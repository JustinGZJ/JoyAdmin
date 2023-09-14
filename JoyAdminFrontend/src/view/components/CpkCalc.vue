<template>
  <div>
    <div>
      <div style="height: 90px">
        数据（用英文逗号隔开）：
        <input type="text" v-model="input" style="margin: 24px; width: 60%; height: 16px;">
        <div>
          上限：
          <input type="text" v-model="usl" style="margin: 24px; width: 60px; height: 16px;" placeholder="上限">
          下限：
          <input type="text" v-model="lsl" style="margin: 24px; width: 60px; height: 16px;" placeholder="下限">
        </div>
        <radio-group>
          <Radio v-model="isSample" label="true">样本</Radio>
          <Radio v-model="isSample" label="false">总体</Radio>
        </radio-group>
      </div>
      <div style=" height: 90px; float: right; ">
        <p>总长度：{{ data.length }}</p>
        <p>总 和： {{ sum }}</p>
        <p>平均值：{{ average }}</p>
        <p>最大值：{{ max }}</p>
        <p>最小值：{{ min }}</p>
        <p>{{ isSample ? '样本' : '总体' }}方差：{{ variance }}</p>
        <p>{{ isSample ? '样本' : '总体' }}标准差：{{ standardDeviation }}</p>
        <p>一倍标准差范围：{{ standarDevRangeOfOne.low + " ~ " + standarDevRangeOfOne.up }}</p>
        <p>二倍标准差范围：{{ standarDevRangeOfTwo.low + " ~ " + standarDevRangeOfTwo.up }}</p>
        <p>三倍标准差范围：{{ standarDevRangeOfThree.low + " ~ " + standarDevRangeOfThree.up }}</p>
        <p>CPL：{{ cpl }}</p>
        <p>CPU：{{ cpu }}</p>
        <p>CP：{{ cp }}</p>
        <p>CA：{{ ca }}</p>
        <p>CPK：{{ cpk }}</p>
        <p>CPK等级：{{ cpkLevel }}</p>
      </div>
    </div>
    <div ref="chart" id="chart" style="width:400px;height:762px; clear: both;"></div>
    <div>
    </div>
  </div>
</template>

<script>
import echarts from 'echarts'
import json from '@/config/json'

export default {
  props: {
    // 这里是组件 props 的定义
    usl: {
      type: Number,
      default: 0
    },
    lsl: {
      type: Number,
      default: 0
    },
    input: {
      type: String,
      default: ''
    }
  },

  data () {
    return {
      // 首页正态曲线的计算公式
      // eslint-disable-next-line no-useless-escape
      mathJax: '`f(x) = (1 / (\sqrt {2\pi} \sigma)) e^(-(x-\mu)^2/(2\sigma^2))`',
      // 数据源（从隔壁的 js 文件中引入）
      json: {},
      // 页面中输入的数据。初始默认为 json 文件
      // 是否为样本数据
      isSample: true
    }
  },
  watch: {
    // 监听输入框，即时更新图形
    input (val) {
      this.input = val
      this.loadChartsSeried() // 加载数据
      this.initChartsData('chart') // 生成 Echarts 图
    }
  },
  computed: {
    data: {
      get () {
        // 因为数组的监听不太好处理，所以多转换一次
        return this.input.split(',').map(t => parseFloat(t))
      },
      set (v) {
        // 如果给 data 赋值，只能是数组类型
        if (!(v instanceof Array)) return
        // 同步更新 input 的值
        this.input = v.join(',')
      }
    },
    dataOrderBy () {
      const data = this.data.concat([]) // 为防止 sort 方法修改原数组，对原数组进行拷贝，操作副本。
      return data.sort((a, b) => a - b)
    },

    dataAfterClean () {
      let res = {}
      // eslint-disable-next-line no-unused-vars
      let data = this.dataOrderBy
      for (let i = 0; i < this.data.length; i++) {
        let key = parseFloat(this.data[i]).toFixed(1) // 这里保留 1 位小数
        if (key !== 'NaN' && parseFloat(key) === 0) {
          key = '0.0'
        } // 这个判断用来处理保留小数位后 -0.0 和 0.0 判定为不同 key 的 bug
        if (res[key]) {
          res[key] += 1
        } else {
          res[key] = 1
        }
      }
      console.log(res)
      return res
    },
    /**
     * @Description： 数据整理。返回源数据所有值（排序后）
     * @Author： ChengduMeng
     * @Date： 2020-11-28 14:35:52
     * */
    dataAfterCleanX () {
      return Object.keys(this.dataAfterClean).sort((a, b) => a - b).map(t => parseFloat(t)
        .toFixed(1)) // 保留 1 位小数
      // return Object.keys(this.dataAfterClean) // 不保证顺序一致
    },
    /**
     * @Description： 数据整理。返回源数据所有值对应的频率（排序后） -- 与 dataAfterCleanX 顺序一致
     * @Author： ChengduMeng
     * @Date： 2020-11-28 13:59:12
     * */
    dataAfterCleanY () {
      let r = []
      for (let i = 0; i < this.dataAfterCleanX.length; i++) {
        r.push(this.dataAfterClean[this.dataAfterCleanX[i]])
      }
      return r
    },
    /**
     * @Description： 数据整理。返回源数据所有值对应的频率，刻度更细致（保留 2 位小数） -- 与 dataAfterCleanX 顺序一致
     * @Author： ChengduMeng
     * @Date： 2020-11-29 13:59:22
     * */
    dataAfterCleanXSub () {
      let r = []
      for (let i = parseFloat(this.min.toFixed(1)); i <= parseFloat(this.max.toFixed(1)); i +=
        0.01) {
        r.push(i.toFixed(2))
      }
      console.log(r)
      return r
    },

    /**
     * @Description： 计算平均数。这里的平均数指的是数学期望、算术平均数
     * @Author： ChengduMeng
     * @Date： 2020-11-27 15:24:14
     * */
    sum () {
      if (this.data.length === 0) return 0
      return this.data.reduce((prev, curr) => prev + curr)
    },
    /**
     * @Description： 计算平均数。这里的平均数指的是数学期望、算术平均数
     * @Author： ChengduMeng
     * @Date： 2020-11-27 15:26:03
     * */
    average () {
      return this.sum / this.data.length
    },
    /**
     * @Description： 计算众数
     * @Author： ChengduMeng
     * @Date： 2020-11-27 15:26:03
     * */
    mode () {
      return 0
    },
    /**
     * @Description： 计算中位数
     * @Author： ChengduMeng
     * @Date： 2020-11-27 15:26:03
     * */
    median () {
      const data = this.dataOrderBy
      return (data[(data.length - 1) >> 1] + data[data.length >> 1]) / 2
    },
    /**
     * @Description： 计算偏差
     * @Author： ChengduMeng
     * @Date： 2020-11-27 15:26:03
     * */
    deviation () {
      // 1、求平均数
      const avg = this.average
      // 2、返回偏差。 f(x) = x - avg
      return this.data.map(x => x - avg)
    },
    /**
     * @Description： 计算总体/样本方差
     * @Author： ChengduMeng
     * @Date： 2020-11-27 15:26:03
     * */
    variance () {
      if (this.data.length === 0) return 0
      // 1、求偏差
      const dev = this.deviation
      // 2、求偏差平方和
      const sumOfSquOfDev = dev.map(x => x * x).reduce((x, y) => x + y)
      // 3、返回方差
      return sumOfSquOfDev / (this.isSample ? (this.data.length - 1) : this.data.length)
    },
    /**
     * @Description： 计算总体/样本标准差
     * @Author： ChengduMeng
     * @Date： 2020-11-27 15:26:03
     * */
    standardDeviation () {
      return Math.sqrt(this.variance)
    },
    /**
     * @Description： 计算一倍标准差范围
     * @Author： ChengduMeng
     * @Date： 2020-11-27 15:26:03
     * */
    standarDevRangeOfOne () {
      return {
        low: this.average - 1 * this.standardDeviation,
        up: this.average + 1 * this.standardDeviation
      }
    },
    /**
     * @Description： 计算三倍标准差范围
     * @Author： ChengduMeng
     * @Date： 2020-11-27 15:29:43
     * */
    standarDevRangeOfTwo () {
      return {
        low: this.average - 2 * this.standardDeviation,
        up: this.average + 2 * this.standardDeviation
      }
    },
    /**
     * @Description： 计算三倍标准差范围
     * @Author： ChengduMeng
     * @Date： 2020-11-27 15:30:49
     * */
    standarDevRangeOfThree () {
      return {
        low: this.average - 3 * this.standardDeviation,
        up: this.average + 3 * this.standardDeviation
      }
    },
    /**
     * @Description： 计算最小值
     * @Author： ChengduMeng
     * @Date： 2020-11-28 13:19:06
     * */
    min () {
      return Math.min.apply(null, this.data)
    },
    /**
     * @Description： 计算最大值
     * @Author： ChengduMeng
     * @Date： 2020-11-28 13:21:16
     * */
    max () {
      return Math.max.apply(null, this.data)
    },
    /**
     * @Description： 正态分布(高斯分布)计算公式
     * @Author： ChengduMeng
     * @Date： 2020-11-28 13:46:18
     * */
    normalDistribution () {
      // 计算公式： `f(x) = (1 / (\sqrt {2\pi} \sigma)) e^(-(x-\mu)^2/(2\sigma^2))`
      // return (1 / Math.sqrt(2 * Math.PI) * a) * (Math.exp(-1 * ((x - u) * (x - u)) / (2 * a * a)))
      let res = []
      for (let i = 0; i < this.dataAfterCleanX.length; i++) {
        const x = this.dataAfterCleanX[i]
        const a = this.standardDeviation
        const u = this.average
        const y = (1 / (Math.sqrt(2 * Math.PI) * a)) * (Math.exp(-1 * ((x - u) * (x - u)) / (2 *
          a * a)))
        res.push(y)
        if (x === 11.8) {
          console.log(y)
        } // 正态分布峰值，用于验证
      }
      return res
    },
    // CPK计算
    cpk () {
      return Math.min(this.cpl, this.cpu)
    },
    // cpl计算
    cpl () {
      return (this.average - this.lsl) / (3 * this.standardDeviation)
    },
    // cpu计算
    cpu () {
      return (this.usl - this.average) / (3 * this.standardDeviation)
    },
    // sc 规格中心
    sc () {
      return (this.usl + this.lsl) / 2
    },
    // ca计算
    ca () {
      return Math.abs(this.average - this.sc) / (0.5 * (this.usl - this.lsl))
    },
    cp () {
      return (this.usl - this.lsl) / (6 * this.standardDeviation)
    },
    // cpk等级
    cpkLevel () {
      if (this.cpk >= 1.67) {
        return 'A'
      } else if (this.cpk >= 1.33) {
        return 'B'
      } else if (this.cpk >= 1) {
        return 'C'
      } else if (this.cpk >= 0.67) {
        return 'D'
      } else if (this.cpk >= 0.33) {
        return 'E'
      } else {
        return 'F'
      }
    }
  },
  created () {
  },
  mounted () {
    this.loadChartsSeried() // 加载数据
    this.initChartsData('chart') // 生成 Echarts 图

    console.log(`总长度： ${this.data.length}`)
    console.log(`总  和： ${this.sum}`)
    console.log(`平均值： ${this.average}`)
    console.log(`最大值： ${this.max}`)
    console.log(`最小值： ${this.min}`)
    console.log(`${this.isSample ? '样本' : '总体'}方差 ${this.variance}`)
    console.log(`${this.isSample ? '样本' : '总体'}标准差： ${this.standardDeviation}`)
    // console.log(`正态分布频率： ${this.normalDistribution}`)
    console.log(`一倍标准差范围： ${this.standarDevRangeOfOne.low}>>> ${this.standarDevRangeOfOne.up}`)
    console.log(`二倍标准差范围： ${this.standarDevRangeOfTwo.low}>>> ${this.standarDevRangeOfTwo.up}`)
    console.log(`三倍标准差范围： ${this.standarDevRangeOfThree.low} >>>${this.standarDevRangeOfThree.up}`)
  },

  methods: {
    /**
     * @Description： 加载 Echarts 图所需数据
     *
     * @Params url
     *
     * @return
     * @Author： ChengduMeng
     * @Date： 2020-11-27 13:24:26
     * */
    loadChartsSeried () {
      this.json = json
    },

    /**
     * @Description： 生成 Echarts 图
     *
     * @Params ref：容器
     *
     * @return
     * @Author： ChengduMeng
     * @Date： 2020-11-27 13:23:26
     * */
    initChartsData (ref) {
      let chart = this.$refs[ref]
      if (!chart) return
      chart = echarts.init(chart)
      // Echarts 图的配置
      let options = {
        // Echarts 图 -- 标题
        // Echarts 图 -- 工具
        tooltip: {},
        // Echarts 图 -- 图例
        legend: {
          data: ['f(x)']
        },
        // Echarts 图 -- x 坐标轴刻度 -- 正态分布数值
        xAxis: [{
          // name : "标准刻度(0.1)",
          data: this.dataAfterCleanX
          // min: this.min,
          // max: this.max
        }],
        // Echarts 图 -- y 坐标轴刻度
        yAxis: [{
          type: 'value',
          name: '频数',
          position: 'left',
          // 网格线
          splitLine: {
            show: false
          },
          axisLine: {
            lineStyle: {
              color: 'orange'
            }
          },
          axisLabel: {
            formatter: '{value}'
          }
        },
        {
          type: 'value',
          name: '概率',
          position: 'right',
          // 网格线
          splitLine: {
            show: false
          },
          axisLine: {
            lineStyle: {
              color: 'black'
            }
          },
          axisLabel: {
            formatter: '{value}'
          }
        }
        ],
        // Echarts 图 -- y 轴数据
        series: [{
          name: '源数据', // y 轴名称
          type: 'bar', // y 轴类型
          yAxisIndex: 0,
          barGap: 0,
          barWidth: 27,
          itemStyle: {
            normal: {
              show: true,
              color: 'rgba(255, 204, 0,.3)', // 柱子颜色
              borderColor: '#FF7F50' // 边框颜色
            }
          },
          data: this.dataAfterCleanY // y 轴数据 -- 源数据
        }, {
          name: '正态分布', // y 轴名称
          type: 'line', // y 轴类型
          // symbol: 'none', //去掉折线图中的节点
          smooth: true, // true 为平滑曲线
          yAxisIndex: 1,
          data: this.normalDistribution, // y 轴数据 -- 正态分布
          // 警示线
          markLine: {
            symbol: ['none'], // 箭头方向
            lineStyle: {
              type: 'silent',
              color: 'green'
            },
            itemStyle: {
              normal: {
                show: true,
                color: 'black'
              }
            },
            label: {
              show: true,
              position: 'middle'
            },
            data: [{
              name: '一倍标准差',
              xAxis: this.standarDevRangeOfOne.low.toFixed(1),
              // 当 n 倍标准差在坐标轴外时，将其隐藏，否则它会默认显示在最小值部分，容易引起混淆
              lineStyle: {
                opacity: (this.min > this.standarDevRangeOfOne
                  .low) ? 0 : 1
              },
              label: {
                show: !(this.min > this.standarDevRangeOfOne.low)
              }
            }, {
              name: '一倍标准差',
              xAxis: this.standarDevRangeOfOne.up.toFixed(1),
              lineStyle: {
                opacity: (this.max < this.standarDevRangeOfOne.up)
                  ? 0 : 1
              },
              label: {
                show: !(this.max < this.standarDevRangeOfOne.up)
              }
            }, {
              name: '二倍标准差',
              xAxis: this.standarDevRangeOfTwo.low.toFixed(1),
              lineStyle: {
                opacity: (this.min > this.standarDevRangeOfTwo
                  .low) ? 0 : 1
              },
              label: {
                show: !(this.min > this.standarDevRangeOfTwo.low)
              }
            }, {
              name: '二倍标准差',
              xAxis: this.standarDevRangeOfTwo.up.toFixed(1),
              lineStyle: {
                opacity: (this.max < this.standarDevRangeOfTwo
                  .up) ? 0 : 1
              },
              label: {
                show: !(this.max < this.standarDevRangeOfTwo.up)
              }
            }, {
              name: '三倍标准差',
              xAxis: this.standarDevRangeOfThree.low.toFixed(1),
              lineStyle: {
                opacity: (this.min > this.standarDevRangeOfThree
                  .low) ? 0 : 1
              },
              label: {
                show: !(this.min > this.standarDevRangeOfThree.low)
              }
            }, {
              name: '三倍标准差',
              xAxis: this.standarDevRangeOfThree.up.toFixed(1),
              lineStyle: {
                opacity: (this.max < this.standarDevRangeOfThree
                  .up) ? 0 : 1
              },
              label: {
                show: !(this.max < this.standarDevRangeOfThree.up)
              }
            }, {
              name: '平均值',
              // type: 'average',
              xAxis: this.average.toFixed(1),
              lineStyle: {
                color: 'red'
              }
            }]
          }
        }]

      }
      chart.setOption(options)
      window.addEventListener('resize', () => {
        chart.resize()
      })
    }
  }
}
</script>

<style scoped>
/* Add your CSS styles here */
</style>
