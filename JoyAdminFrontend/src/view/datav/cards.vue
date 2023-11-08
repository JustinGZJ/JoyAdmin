<template>
  <div id="cards">
    <div class="card-item" v-for="(card) in displayCards" :key="card.title">
      <div class="card-header">
        <div class="card-header-left">{{ card.title }}</div>
        <!--        <div class="card-header-right">{{ "0" + (i + 1) }}</div>-->
      </div>

      <dv-charts class="ring-charts" :option="card.ring"/>
      <div class="card-footer">
        <div class="card-footer-item">
          <div class="footer-title">合格</div>
          <div class="footer-detail">
            <dv-digital-flop
              :config="card.ok"
              style="width: 70%; height: 35px"
            />
          </div>
        </div>
        <div class="card-footer-item">
          <div class="footer-title">不合格</div>
          <div class="footer-detail">
            <dv-digital-flop
              :config="card.ng"
              style="width: 70%; height: 35px"
            />
          </div>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
export default {
  name: 'Cards',
  props: {
    cards: {
      type: Array,
      default: () => new Array(5).fill({
        title: '',
        num: 0,
        total: 1
      })
    }
  },
  watch: {
    // cards(newValue) {
    //   this.createData(newValue)
    // }
  },
  data () {
    return {
      cardIndex:0 
      // cardsData: []
    }
  },
  computed: {
    cardsData () {
      let cards = []
      for (const arrayElement of this.cards) {
        let { title, num, total } = arrayElement
        let percent = total ? Math.round(num * 100 / total) : 100
        cards.push({
          title: title,
          ng: {
            number: [total - num],
            content: '{nt}',
            textAlign: 'right',
            style: {
              fill: '#ea6027',
              fontWeight: 'bold'
            }
          },
          ok: {
            number: [num],
            content: '{nt}',
            textAlign: 'right',
            style: {
              fill: '#26fcd8',
              fontWeight: 'bold'
            }
          },
          ring: {
            series: [
              {
                type: 'gauge',
                startAngle: -Math.PI / 2,
                endAngle: Math.PI * 1.5,
                arcLineWidth: 13,
                radius: '80%',
                data: [
                  { name: '', value: percent }
                ],
                axisLabel: {
                  show: false
                },
                axisTick: {
                  show: false
                },
                pointer: {
                  show: false
                },
                backgroundArc: {
                  style: {
                    stroke: '#224590'
                  }
                },
                details: {
                  show: true,
                  formatter: '{value}%',
                  style: {
                    fill: '#1ed3e5',
                    fontSize: 20
                  }
                }
              }
            ],
            color: ['#03d3ec']
          }
        })
      }
      return cards
    },
    displayCards() {
    return this.cardsData.slice(this.cardIndex, this.cardIndex + 5);
  }
  },
  created: function () {
  },
  methods: {
    // 订阅主题
    createData (array) {
      let cards = []
      for (const arrayElement of array) {
        let { title, num, total } = arrayElement
        let percent = total ? Math.round(num * 100 / total) : 100
        cards.push({
          title: title,
          ng: {
            number: [total - num],
            content: '{nt}',
            textAlign: 'right',
            style: {
              fill: '#ea6027',
              fontWeight: 'bold'
            }
          },
          ok: {
            number: [num],
            content: '{nt}',
            textAlign: 'right',
            style: {
              fill: '#26fcd8',
              fontWeight: 'bold'
            }
          },
          ring: {
            series: [
              {
                type: 'gauge',
                startAngle: -Math.PI / 2,
                endAngle: Math.PI * 1.5,
                arcLineWidth: 13,
                radius: '80%',
                data: [
                  { name: '', value: percent }
                ],
                axisLabel: {
                  show: false
                },
                axisTick: {
                  show: false
                },
                pointer: {
                  show: false
                },
                backgroundArc: {
                  style: {
                    stroke: '#224590'
                  }
                },
                details: {
                  show: true,
                  formatter: '{value}%',
                  style: {
                    fill: '#1ed3e5',
                    fontSize: 20
                  }
                }
              }
            ],
            color: ['#03d3ec']
          }
        })
      }
      this.cardsData = cards
    }
  },
  beforeMount () {
    this.createData(this.cardsData)
  },
  mounted () {
    setInterval(() => {
    this.cardIndex+=5;
    if (this.cardIndex >= this.cardsData.length) {
      this.cardIndex = 0; 
    }
  }, 3000);
  }
}
</script>

<style lang="less">
#cards {
  display: flex;
  justify-content: space-between;
  height: 45%;

  .card-item {
    background-color: rgba(6, 30, 93, 0.5);
    border-top: 2px solid rgba(1, 153, 209, 0.5);
    width: 19%;
    display: flex;
    flex-direction: column;
  }

  .card-header {
    display: flex;
    height: 20%;
    align-items: center;
    justify-content: space-between;

    .card-header-left {
      font-size: 18px;
      font-weight: bold;
      padding-left: 20px;
    }

    .card-header-right {
      padding-right: 20px;
      font-size: 40px;
      color: #03d3ec;
    }
  }

  .ring-charts {
    height: 55%;
  }

  .card-footer {
    height: 25%;
    display: flex;
    align-items: center;
    justify-content: space-around;
  }

  .card-footer-item {
    padding: 5px 10px 0px 10px;
    box-sizing: border-box;
    width: 40%;
    background-color: rgba(6, 30, 93, 0.7);
    border-radius: 3px;

    .footer-title {
      font-size: 15px;
      margin-bottom: 5px;
    }

    .footer-detail {
      font-size: 20px;
      color: #1294fb;
      display: flex;
      font-size: 18px;
      align-items: center;

      .dv-digital-flop {
        margin-right: 5px;
      }
    }
  }
}
</style>
