<template>
  <q-layout view="hHh lpR fFf">

    <q-header elevated class="bg-primary text-white q-pa-sm bg-deep-purple-7" height-hint="98">
      <q-toolbar>
        <q-toolbar-title>
          SPAMBOT
        </q-toolbar-title>
      </q-toolbar>

    </q-header>

    <q-page-container>
      <router-view />
    </q-page-container>

  </q-layout>
</template>

<script>
import { api } from 'boot/axios';

export default {
  methods: {
    sendSpam() {
      api.post('/api/SendSpam')
        .then(() => {
          this.$q.notify({
            color: 'positive',
            position: 'top',
            message: 'Spam sent',
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
  }
}
</script>