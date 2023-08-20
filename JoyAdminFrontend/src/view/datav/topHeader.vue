<template>
  <div id="top-header">
    <dv-decoration-8 class="header-left-decoration" />
    <dv-decoration-5 class="header-center-decoration" />
    <dv-decoration-8 class="header-right-decoration" :reverse="true" />
    <div class="center-title">{{title}}</div>
  </div>
</template>

<script>
export default {
  name: 'TopHeader',
  props: {
    title: {
      type: String,
      default: '电机生产线'
    }
  }
}
</script>

<style lang="less">
#top-header {
  position: relative;
  width: 100%;
  height: 50px;
  display: flex;
  justify-content: space-between;
  flex-shrink: 0;

  .header-center-decoration {
    width: 40%;
    height: 30px;
    margin-top: 15px;
  }

  .header-left-decoration,
  .header-right-decoration {
    width: 25%;
    height: 30px;
  }

  .center-title {
    position: absolute;
    font-size: 15px;
    font-weight: bold;
    left: 50%;
    top: 7px;
    transform: translateX(-50%);
  }
}
</style>
