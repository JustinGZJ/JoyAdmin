<template>
  <Div>
    <Card style="margin-bottom: 10px">
      <Input v-model="code" placeholder="二维码" style="width: 200px"/>
      <Button type="primary" @click="query" icon="md-add" style="margin-left:10px ">查询</Button>
    </Card>
  </Div>
</template>
<script >
export default {
  data () {
    return {
      code: ''
    }
  },
  methods: {
    query () {

    }
  }
}
</script>
<style scoped lang="less">

</style>
