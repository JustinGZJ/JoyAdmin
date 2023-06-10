<template>
  <div>
    <Card title="修改密码" style="width: 500px">
      <Row>
        <Input
          v-model="OldPwd"
          type="password"
          size="large"
          placeholder="原密码"
        />
      </Row>
      <br />
      <Row>
        <Input
          v-model="NewPwd"
          type="password"
          size="large"
          placeholder="新密码"
        />
      </Row>
      <br />
      <Row>
        <Input
          v-model="ReNewPwd"
          type="password"
          size="large"
          placeholder="确认新密码"
        />
      </Row>
      <br />
      <Row>
        <Button
          size="large"
          type="primary"
          :loading="loading"
          @click="UpdatePwd"
          >确认修改</Button
        >
      </Row>
    </Card>
  </div>
</template>

<script>
import { Message } from 'view-design'
import { UpdatePwd } from '@/api/rabc'
import { mapActions } from 'vuex'
export default {
  name: 'updatepwd',
  data () {
    return {
      loading: false,
      OldPwd: '',
      NewPwd: '',
      ReNewPwd: ''
    }
  },
  methods: {
    ...mapActions(['handleLogOut']),
    UpdatePwd () {
      if (!this.OldPwd) {
        Message.error('原密码不能为空')
        return
      }
      if (!this.NewPwd) {
        Message.error('新密码不能为空')
        return
      }
      if (!this.ReNewPwd) {
        Message.error('确认新密码不能为空')
        return
      }
      if (this.ReNewPwd != this.NewPwd) {
        Message.error('两次输入的密码不一致')
        return
      }
      var reqData = {
        OldPassword: this.OldPwd,
        Password: this.NewPwd
      }
      this.loading = true
      UpdatePwd(reqData).then((res) => {
        this.loading = false
        const data = res.data
        if (data.StatusCode == 200) {
          Message.success('密码修改成功，请重新登录')
          this.handleLogOut().then(() => {
            this.$router.push({
              name: 'login'
            })
          })
        } else {
          Message.error({ title: '提示', content: data.Errors })
        }
      })
    }
  }
}
</script>

<style>
</style>
