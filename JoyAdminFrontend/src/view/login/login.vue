<style lang="less">
@import "./login.less";
</style>

<template>
  <div class="login">
    <div class="login-left">
      <div class="login-left-title">{{ subcompany }}</div>
      <div class="login-left-form">
        <div class="login-left-form-title">{{ title }}</div>
        <img style="width: 400px" src="../../assets/images/login-form.png" />
      </div>
    </div>
    <div class="login-con">
      <Card title="欢迎登录" :bordered="false">
        <div class="form-con">
          <Form
            ref="loginForm"
            :model="form"
            :rules="rules"
            @keydown.enter.native="handleSubmit"
          >
            <FormItem prop="userName">
              <Input
                size="large"
                v-model="form.userName"
                placeholder="请输入用户名"
              >
                <span slot="prepend">
                  <Icon :size="16" type="ios-person"></Icon>
                </span>
              </Input>
            </FormItem>
            <FormItem prop="password">
              <Input
                size="large"
                type="password"
                v-model="form.password"
                placeholder="请输入密码"
              >
                <span slot="prepend">
                  <Icon :size="14" type="md-lock"></Icon>
                </span>
              </Input>
            </FormItem>
            <FormItem>
              <Button
                size="large"
                @click="handleSubmit"
                :loading="loading"
                type="primary"
                long
                >登录</Button
              >
            </FormItem>
          </Form>
        </div>
      </Card>
    </div>
    <div class="login-footer"></div>
  </div>
</template>

<script>
import { mapActions } from 'vuex'
import { title, subcompany, company } from '@/libs/util'
export default {
  data () {
    return {
      title,
      subcompany,
      company,
      loading: false,
      form: {
        userName: '',
        password: ''
      },
      rules: {
        userName: [
          { required: true, message: '账号不能为空', trigger: 'blur' }
        ],
        password: [
          { required: true, message: '密码不能为空', trigger: 'blur' }
        ]
      }
    }
  },
  methods: {
    ...mapActions(['handleLogin', 'GetUserInfo']),

    handleSubmit () {
      let vm = this
      vm.loading = true
      vm.handleLogin(vm.form)
        .then((res) => {
          vm.loading = false
          if (res.StatusCode == 200) {
            vm.GetUserInfo().then((res2) => {
              if (res2.StatusCode == 200) {
                vm.$router.push({
                  name: vm.$config.homeName
                })
              }
            })
          } else {
            vm.$Notice.error({
              title: res.Errors
            })
          }
        })
        .catch(() => {
          vm.loading = false
        })
    }
  }
}
</script>

<style>
</style>
