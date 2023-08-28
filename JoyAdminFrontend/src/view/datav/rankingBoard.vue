<template>
  <div id="ranking-board">
    <div class="ranking-board-title">工站周期时间</div>
    <dv-scroll-ranking-board :config="config">
    </dv-scroll-ranking-board>
  </div>
</template>

<script>
export default {
  name: 'RankingBoard',
  props: {
    ranking: {
      type: Object,
      default: () => {}
    }
  },
  data () {
    return {
      config: {
        data: [
          {
            name: '联轴机装配',
            value: 70
          },
          {
            name: '转子装外衬套',
            value: 120
          },
          {
            name: '端盖铆压轴承',
            value: 78
          },
          {
            name: '定子性能测试',
            value: 66
          },
          {
            name: '装磁石',
            value: 80
          },
          {
            name: '入壳产品铆压',
            value: 45
          },
          {
            name: '定子入机壳',
            value: 29
          },
          {
            name: '激光焊接',
            value: 29
          },
          {
            name: '定子拼园',
            value: 29
          },
          {
            name: '电机合装',
            value: 29
          },
          {
            name: '汇流桥装配',
            value: 29
          }
        ],
        rowNum: 11
      }
    }
  },
  created () {

  },
  mounted () {
  },
  watch: {
    ranking (newValue) {
      let data = []
      for (const newValueKey in newValue) {
        data.push({
          name: newValueKey,
          value: Math.round(newValue[newValueKey] * 10) / 10
        })
      }
      this.config = {
        data: data,
        rowNum: data.length
      }
    }
  },
  methods: {
  }
}
</script>

<style lang="less">
#ranking-board {
  width: 20%;
  box-shadow: 0 0 3px blue;
  display: flex;
  flex-direction: column;
  background-color: rgba(6, 30, 93, 0.5);
  border-top: 2px solid rgba(1, 153, 209, 0.5);
  box-sizing: border-box;
  padding: 0px 30px;

  .ranking-board-title {
    font-weight: bold;
    height: 50px;
    display: flex;
    align-items: center;
    font-size: 20px;
  }

  .dv-scroll-ranking-board {
    flex: 1;
  }
}
</style>
