<template>
  <q-page class="flex flex-center">
    <q-form
      @submit="sendData"
      class="q-gutter-lg q-pa-sm"
      style="width: 800px;"
    >
      <div class="row">
        <q-select v-model="form.email" :options="emails" option-label="email" label="Emails" class="col q-mr-sm" />
        <q-input
          class="col"
          v-model="form.title"
          label="Email title"
          lazy-rules
          :rules="[ val => val && val.length > 0 || 'Please type something']"
        />
      </div>
      <div class="col">
        <q-input
          v-model="form.description"
          label="Email description"
          type="textarea"
          style="max-height: 400px"
        />
      </div>
      <q-btn type="submit" color="primary" icon="mail" label="Add spam" />
    </q-form>
  </q-page>
</template>

<script>
import { api } from 'boot/axios';

export default {
  name: 'AddSpam',
  data () {
    return {
      form: {
        email: '',
        title: '',
        description: '',
      },
      emails: [],
    }
  },
  methods: {
    loadEmails () {
      api.get('/api/Receiver')
        .then((response) => {
          this.emails = response.data
        })
        .catch(() => {
          this.$q.notify({
            color: 'negative',
            position: 'top',
            message: 'Loading failed',
            icon: 'report_problem'
          })
      })
    },
    sendData() {
      api.post('/api/Email', this.form)
        .then((response) => {
          this.$q.notify({
            color: 'positive',
            position: 'top',
            message: 'Data sent',
          })
        })
        .catch(() => {
          this.$q.notify({
            color: 'negative',
            position: 'top',
            message: 'Sending failed',
            icon: 'report_problem'
          })
        })
    }
  },
  created() {
    this.loadEmails()
  }
}

</script>
