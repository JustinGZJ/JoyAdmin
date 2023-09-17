<template>
  <div id="ranking-board">
    <div class="ranking-board-title">物料剩余</div>
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
  computed: {
    config () {
      let data = []
      for (const newValueKey in this.ranking) {
        data.push({
          name: newValueKey,
          value: Math.round(this.ranking[newValueKey] * 10) / 10
        })
      }
      return {
        data: data,
        rowNum: 11,
        sort: false,
        unit: ''
      }
    }
  },
  created () {

  },
  mounted () {
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
