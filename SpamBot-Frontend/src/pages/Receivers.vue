<template>
  <q-page class="flex flex-center q-pa-sm">
    <q-table
      title="SPAM RECEIVERS"
      :data="receivers"
      :columns="columns"
      row-key="name"
      style="width: 800px; height: 600px;"
    />
  </q-page>
</template>

<script>
import { api } from 'boot/axios';

export default {
  name: 'Receivers',
  data () {
    return {
      receivers: [],
      columns: [
        { name: 'email', label: 'Email', field: 'email', sortable: true, align: 'left' },
        { name: 'fullname', label: 'Full Name', field: 'fullname', sortable: true, align: 'left' },
      ]
    }
  },
  methods: {
    loadSpam () {
      api.get('/api/Receiver')
        .then((response) => {
          this.receivers = response.data
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
  }
}

</script>
