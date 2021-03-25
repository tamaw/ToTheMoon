<template>
  <div class="container">
    <h1 class="mt-1">Your cryptocurrency of choice</h1>

    <div class="row">
      <div class="form-group row">
        <label class="col-4 col-form-label" for="preferredCoin">Coin:</label>
        <div class="col-8">
          <select
            id="preferredCoin"
            name="preferredCoin"
            required="required"
            class="custom-select"
            v-model="preferredCoin"
            @change="onChange($event)"
          >
            <option v-for="coin in coins" :value="coin" :key="coin">
              {{ coin }}
            </option>
          </select>
        </div>
      </div>
    </div>

    <hr />

    <h1 class="mt-1">{{ preferredCoin }}</h1>
    <div class="row">Ask Price: ${{ askPrice }}</div>

    <div class="row">Bid Price: ${{ bidPrice }}</div>

    <div class="row">Rate Price: {{ rate }}</div>
    <div v-if="showDifference">
      <div class="row">Previous Ask Price: ${{ previousAskPrice }}</div>
      <div class="row">Difference: {{ percentDifference }}</div>
    </div>

    <div class="row">
      <button type="button" class="btn btn-primary" @click="onRefreshClick">
        Refresh
      </button>
    </div>
  </div>
</template>
<script>
export default {
  props: {},
  data() {
    return {
      coins: ["BTC", "ETH", "XRP"],
      preferredCoin: null,
      askPrice: "...",
      bidPrice: "...",
      rate: "...",
      previousAskPrice: null,
      percentDifference: "...",
      showDifference: false,
    };
  },
  async created() {
    await this.$http.get("/api/PreferredCoin").then(async (res) => {
      this.preferredCoin = res.data.preferredCoin;
      await this.priceUpdate();
    });
  },
  computed: {
  },
  methods: {
    async onChange(e) {
      this.showDifference = false;

      this.$http
        .post("/api/PreferredCoin", { coin: e.target.value })
        .then(async (res) => {
          this.preferredCoin = res.data.changedTo;

          this.priceUpdate();
        });
    },
    onRefreshClick(e) {
      this.priceUpdate();
      this.showDifference = true;
    },
    calcDifference() {
        let difference = (this.askPrice - this.previousAskPrice) / this.previousAskPrice;
        this.percentDifference = parseFloat(difference).toFixed(10) + "%";
    },
    async priceUpdate() {
      this.previousAskPrice = this.askPrice;
      this.askPrice = "...";
      this.bidPrice = "...";
      this.rate = "...";
      this.percentDifference = "...";

      await this.$http
        .get("/api/CoinPrice/" + this.preferredCoin)
        .then((res) => {
          this.askPrice = res.data.ask;
          this.bidPrice = res.data.bid;
          this.rate = res.data.rate;
        });
      this.calcDifference();
    },
  },
};
</script>
