<template>
  <div id="status">
    <dv-full-screen-container>
      <TopHeader title="生产情况" />
      <product-by-hour />
    </dv-full-screen-container>
  </div>
</template>

<script>
// eslint-disable-next-line camelcase
import TopHeader from '@/view/datav/topHeader.vue'
import ProductByHour from '@/view/datav/ProductByHour.vue'

export default {
  name: 'ProductView',
  components: { ProductByHour, TopHeader }
}
</script>

<style scoped>
#status {
  width: 100%;
  height: 100%;
  background-color: #030409;
  color: #fff;

  #dv-full-screen-container {
    background-image: url("./img/bg.png");
    background-size: 100% 100%;
    box-shadow: 0 0 3px blue;
    display: flex;
    flex-direction: column;
  }
}
</style>
